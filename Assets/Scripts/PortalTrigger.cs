using System;
using System.Collections;
using System.Collections.Generic;
using RPG.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Trigger

{
    public class PortalTrigger : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A, B, C, D, E
        }

        [SerializeField] string sceneToLoad;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeOutTime = 0.5f;
        [SerializeField] float fadeInTime = 1f;
        [SerializeField] float fadeWaitTime = 0.5f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            
            DontDestroyOnLoad(gameObject);
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeOutTime);
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            PortalTrigger otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);
            yield return new WaitForSeconds(fadeWaitTime);
            yield return fader.FadeIn(fadeInTime);
            Destroy(gameObject);
        }

        private void UpdatePlayer(PortalTrigger otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.transform.position = otherPortal.spawnPoint.position;
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }

        private PortalTrigger GetOtherPortal()
        {
            foreach (PortalTrigger portal in FindObjectsOfType<PortalTrigger>())
            {
                if (portal == this) continue;
                if (portal.destination != destination) continue;

                return portal;
            }

            return null;
        }
    }
}