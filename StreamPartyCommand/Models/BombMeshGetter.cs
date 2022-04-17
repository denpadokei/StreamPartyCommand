﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace StreamPartyCommand.Models
{
    public class BombMeshGetter : IInitializable, IDisposable
    {
        public static GameObject BombGO { get; private set; }
        private static MeshRenderer _bombMesh;
        private bool disposedValue;

        public static MeshRenderer BombMesh
        {
            get
            {
                if (_bombMesh == null && BombGO != null) {
                    var mesh = BombGO.GetComponentsInChildren<MeshRenderer>(true).FirstOrDefault(x => x.name == "Mesh");
                    if (mesh != null) {
                        _bombMesh = GameObject.Instantiate(mesh);
                        _bombMesh.enabled = false;
                    }
                }
                return _bombMesh;
            }
        }
        public void Initialize()
        {
            try {
                var bomb = Resources.FindObjectsOfTypeAll<MonoBehaviour>().FirstOrDefault(x => x.name == "BombNote").gameObject;
                BombGO = GameObject.Instantiate(bomb);
                foreach (var component in BombGO.GetComponentsInChildren<Component>(true)) {
                    if (component is BombNoteController bombNoteController) {
                        GameObject.Destroy(bombNoteController);
                    }
                    if (component is SphereCollider sphereCollider) {
                        GameObject.Destroy(sphereCollider);
                    }
                    if (component is SphereCuttableBySaber sphereCuttableBySaber) {
                        GameObject.Destroy(sphereCuttableBySaber);
                    }
                }
                BombGO.SetActive(false);
            }
            catch (Exception e) {
                Logger.Error(e);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue) {
                if (disposing) {
                    if (BombMesh != null) {
                        GameObject.Destroy(BombMesh);
                    }
                    if (BombGO != null) {
                        GameObject.Destroy(BombGO);
                    }
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
