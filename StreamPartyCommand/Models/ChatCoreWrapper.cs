using ChatCore;
using ChatCore.Interfaces;
using ChatCore.Services;
using ChatCore.Services.Twitch;
using StreamPartyCommand.Events;
using System;
using System.Collections.Concurrent;
using Zenject;

namespace StreamPartyCommand.Models
{
    public class ChatCoreWrapper : IInitializable, IDisposable
    {
        public ChatCoreInstance CoreInstance { get; private set; }
        public ChatServiceMultiplexer MultiplexerInstance { get; private set; }
        public TwitchService TwitchService { get; private set; }
        public ConcurrentQueue<ReceiveMessageEventArgs> RecieveChatMessage { get; } = new ConcurrentQueue<ReceiveMessageEventArgs>();
        public ConcurrentQueue<string> SendMessageQueue { get; } = new ConcurrentQueue<string>();

        public event OnTextMessageReceivedHandler OnMessageReceived;
        public event OnLoginHandler OnLogined;
        public event OnJoinChannelHandler OnJoinChannel;

        private bool disposedValue;


        public void Initialize()
        {
            this.CoreInstance = ChatCoreInstance.Create();
            this.MultiplexerInstance = this.CoreInstance.RunAllServices();
            this.MultiplexerInstance.OnLogin += this.MultiplexerInstance_OnLogin;
            this.MultiplexerInstance.OnJoinChannel += this.MultiplexerInstance_OnJoinChannel;
            this.TwitchService = this.MultiplexerInstance.GetTwitchService();
            this.MultiplexerInstance.OnTextMessageReceived += this.MultiplexerInstance_OnTextMessageReceived;

            this.OnMessageReceived += this.ChatCoreWrapper_OnMessageReceived;
        }

        private void ChatCoreWrapper_OnMessageReceived(object sender, ReceiveMessageEventArgs e)
        {
            this.RecieveChatMessage.Enqueue(e);
        }

        private void MultiplexerInstance_OnTextMessageReceived(IChatService arg1, IChatMessage arg2)
        {
            this.OnMessageReceived?.Invoke(this, new ReceiveMessageEventArgs(arg1, arg2));
        }

        private void MultiplexerInstance_OnJoinChannel(IChatService arg1, IChatChannel arg2)
        {
            this.OnJoinChannel?.Invoke(this, new JoinChannelEventArgs(arg1, arg2));
        }

        private void MultiplexerInstance_OnLogin(IChatService obj)
        {
            this.OnLogined?.Invoke(this, new LoginEventArgs(obj));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue) {
                if (disposing) {
                    this.MultiplexerInstance.OnLogin -= this.MultiplexerInstance_OnLogin;
                    this.MultiplexerInstance.OnJoinChannel -= this.MultiplexerInstance_OnJoinChannel;
                    this.MultiplexerInstance.OnTextMessageReceived += this.MultiplexerInstance_OnTextMessageReceived;

                    this.OnMessageReceived -= this.ChatCoreWrapper_OnMessageReceived;
                }
                this.disposedValue = true;
            }
        }
        public void Dispose()
        {
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

    public delegate void OnTextMessageReceivedHandler(object sender, ReceiveMessageEventArgs e);
    public delegate void OnLoginHandler(object sender, LoginEventArgs e);
    public delegate void OnJoinChannelHandler(object sender, JoinChannelEventArgs e);
}
