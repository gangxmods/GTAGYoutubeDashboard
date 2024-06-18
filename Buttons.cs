using System.Collections;
using UnityEngine;

namespace YoutubeDashboard
{
    public class VButton1 : MonoBehaviour
    {
        public bool CD = false;

        public void Awake() => transform.GetComponent<Renderer>().material = Plugin.Instance.unpressedMat;

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.TryGetComponent<GorillaTriggerColliderHandIndicator>(out GorillaTriggerColliderHandIndicator component) && Time.time > Plugin.Instance.LastPress + Plugin.Debounce)
            {
                if (!Plugin.Instance.CD)
                {
                    if (!CD)
                    {
                        Plugin.Instance.LastPress = Time.time;

                        StartCoroutine(Press());
                        GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(67, component.isLeftHand, 0.05f);
                        GorillaTagger.Instance.StartVibration(component.isLeftHand, GorillaTagger.Instance.tapHapticStrength / 1, GorillaTagger.Instance.tapHapticDuration / 1);
                    }
                }
            }
        }

        private IEnumerator Press()
        {
            CD = true;
            transform.GetComponent<Renderer>().material = Plugin.Instance.pressedMat;
            Plugin.Instance.VideoButton1();
            yield return new WaitForSeconds(0.25f);
            transform.GetComponent<Renderer>().material = Plugin.Instance.unpressedMat;
            CD = false;
        }
    }

    public class VButton2 : MonoBehaviour
    {
        public bool CD = false;

        public void Awake() => transform.GetComponent<Renderer>().material = Plugin.Instance.unpressedMat;

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.TryGetComponent<GorillaTriggerColliderHandIndicator>(out GorillaTriggerColliderHandIndicator component) && Time.time > Plugin.Instance.LastPress + Plugin.Debounce)
            {
                if (!Plugin.Instance.CD)
                {
                    if (!CD)
                    {
                        Plugin.Instance.LastPress = Time.time;

                        StartCoroutine(Press());
                        GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(67, component.isLeftHand, 0.05f);
                        GorillaTagger.Instance.StartVibration(component.isLeftHand, GorillaTagger.Instance.tapHapticStrength / 1, GorillaTagger.Instance.tapHapticDuration / 1);
                    }
                }
            }
        }

        private IEnumerator Press()
        {
            CD = true;
            transform.GetComponent<Renderer>().material = Plugin.Instance.pressedMat;
            Plugin.Instance.VideoButton2();
            yield return new WaitForSeconds(0.25f);
            transform.GetComponent<Renderer>().material = Plugin.Instance.unpressedMat;
            CD = false;
        }
    }

    public class VButton3 : MonoBehaviour
    {
        public bool CD = false;

        public void Awake() => transform.GetComponent<Renderer>().material = Plugin.Instance.unpressedMat;

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.TryGetComponent<GorillaTriggerColliderHandIndicator>(out GorillaTriggerColliderHandIndicator component) && Time.time > Plugin.Instance.LastPress + Plugin.Debounce)
            {
                if (!Plugin.Instance.CD)
                {
                    if (!CD)
                    {
                        Plugin.Instance.LastPress = Time.time;

                        StartCoroutine(Press());
                        GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(67, component.isLeftHand, 0.05f);
                        GorillaTagger.Instance.StartVibration(component.isLeftHand, GorillaTagger.Instance.tapHapticStrength / 1, GorillaTagger.Instance.tapHapticDuration / 1);
                    }
                }
            }
        }

        private IEnumerator Press()
        {
            CD = true;
            transform.GetComponent<Renderer>().material = Plugin.Instance.pressedMat;
            Plugin.Instance.VideoButton3();
            yield return new WaitForSeconds(0.25f);
            transform.GetComponent<Renderer>().material = Plugin.Instance.unpressedMat;
            CD = false;
        }
    }

    public class VButton4 : MonoBehaviour
    {
        public bool CD = false;

        public void Awake() => transform.GetComponent<Renderer>().material = Plugin.Instance.unpressedMat;

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.TryGetComponent<GorillaTriggerColliderHandIndicator>(out GorillaTriggerColliderHandIndicator component) && Time.time > Plugin.Instance.LastPress + Plugin.Debounce)
            {
                if (!Plugin.Instance.CD)
                {
                    if (!CD)
                    {
                        Plugin.Instance.LastPress = Time.time;

                        StartCoroutine(Press());
                        GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(67, component.isLeftHand, 0.05f);
                        GorillaTagger.Instance.StartVibration(component.isLeftHand, GorillaTagger.Instance.tapHapticStrength / 1, GorillaTagger.Instance.tapHapticDuration / 1);
                    }
                }
            }
        }

        private IEnumerator Press()
        {
            CD = true;
            transform.GetComponent<Renderer>().material = Plugin.Instance.pressedMat;
            Plugin.Instance.VideoButton4();
            yield return new WaitForSeconds(0.25f);
            transform.GetComponent<Renderer>().material = Plugin.Instance.unpressedMat;
            CD = false;
        }
    }

    public class VButton5 : MonoBehaviour
    {
        public bool CD = false;

        public void Awake() => transform.GetComponent<Renderer>().material = Plugin.Instance.unpressedMat;

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.TryGetComponent<GorillaTriggerColliderHandIndicator>(out GorillaTriggerColliderHandIndicator component) && Time.time > Plugin.Instance.LastPress + Plugin.Debounce)
            {
                if (!Plugin.Instance.CD)
                {
                    if (!CD)
                    {
                        Plugin.Instance.LastPress = Time.time;

                        StartCoroutine(Press());
                        GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(67, component.isLeftHand, 0.05f);
                        GorillaTagger.Instance.StartVibration(component.isLeftHand, GorillaTagger.Instance.tapHapticStrength / 1, GorillaTagger.Instance.tapHapticDuration / 1);
                    }
                }
            }
        }

        private IEnumerator Press()
        {
            CD = true;
            transform.GetComponent<Renderer>().material = Plugin.Instance.pressedMat;
            Plugin.Instance.VideoButton5();
            yield return new WaitForSeconds(0.25f);
            transform.GetComponent<Renderer>().material = Plugin.Instance.unpressedMat;
            CD = false;
        }
    }

    public class Forward : MonoBehaviour
    {
        public bool CD = false;

        public void Awake() => transform.GetComponent<Renderer>().material = Plugin.Instance.unpressedMat;

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.TryGetComponent<GorillaTriggerColliderHandIndicator>(out GorillaTriggerColliderHandIndicator component) && Time.time > Plugin.Instance.LastPress + Plugin.Debounce)
            {
                if (!Plugin.Instance.CD)
                {
                    if (!CD)
                    {
                        Plugin.Instance.LastPress = Time.time;

                        StartCoroutine(Press());
                        GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(67, component.isLeftHand, 0.05f);
                        GorillaTagger.Instance.StartVibration(component.isLeftHand, GorillaTagger.Instance.tapHapticStrength / 1, GorillaTagger.Instance.tapHapticDuration / 1);
                    }
                }
            }
        }

        private IEnumerator Press()
        {
            CD = true;
            transform.GetComponent<Renderer>().material = Plugin.Instance.pressedMat;
            Plugin.Instance.Forward();
            Plugin.Instance.VButton1.GetComponent<Renderer>().material = Plugin.Instance.unpressedMat;
            Plugin.Instance.VButton2.GetComponent<Renderer>().material = Plugin.Instance.unpressedMat;
            Plugin.Instance.VButton3.GetComponent<Renderer>().material = Plugin.Instance.unpressedMat;
            Plugin.Instance.VButton4.GetComponent<Renderer>().material = Plugin.Instance.unpressedMat;
            Plugin.Instance.VButton5.GetComponent<Renderer>().material = Plugin.Instance.unpressedMat;
            yield return new WaitForSeconds(0.25f);
            transform.GetComponent<Renderer>().material = Plugin.Instance.unpressedMat;
            CD = false;
        }
    }

    public class Mute : MonoBehaviour
    {
        public bool CD = false;

        public void Awake() => transform.GetComponent<Renderer>().material = Plugin.Instance.unpressedMat;

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.TryGetComponent<GorillaTriggerColliderHandIndicator>(out GorillaTriggerColliderHandIndicator component) && Time.time > Plugin.Instance.LastPress + Plugin.Debounce)
            {
                if (!Plugin.Instance.CD)
                {
                    if (!CD)
                    {
                        Plugin.Instance.LastPress = Time.time;

                        StartCoroutine(Press());
                        GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(67, component.isLeftHand, 0.05f);
                        GorillaTagger.Instance.StartVibration(component.isLeftHand, GorillaTagger.Instance.tapHapticStrength / 1, GorillaTagger.Instance.tapHapticDuration / 1);
                    }
                }
            }
        }

        private IEnumerator Press()
        {
            CD = true;
            transform.GetComponent<Renderer>().material = Plugin.Instance.pressedMat;
            Plugin.Instance.Mute();
            yield return new WaitForSeconds(0.25f);
            transform.GetComponent<Renderer>().material = Plugin.Instance.unpressedMat;
            CD = false;
        }
    }

    public class FastForward : MonoBehaviour
    {
        public bool CD = false;

        public void Awake() => transform.GetComponent<Renderer>().material = Plugin.Instance.unpressedMat;

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.TryGetComponent<GorillaTriggerColliderHandIndicator>(out GorillaTriggerColliderHandIndicator component) && Time.time > Plugin.Instance.LastPress + Plugin.Debounce)
            {
                if (!Plugin.Instance.CD)
                {
                    if (!CD)
                    {
                        Plugin.Instance.LastPress = Time.time;

                        StartCoroutine(Press());
                        GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(67, component.isLeftHand, 0.05f);
                        GorillaTagger.Instance.StartVibration(component.isLeftHand, GorillaTagger.Instance.tapHapticStrength / 1, GorillaTagger.Instance.tapHapticDuration / 1);
                    }
                }
            }
        }

        private IEnumerator Press()
        {
            CD = true;
            transform.GetComponent<Renderer>().material = Plugin.Instance.pressedMat;
            Plugin.Instance.FastForwardPress();
            yield return new WaitForSeconds(0.25f);
            transform.GetComponent<Renderer>().material = Plugin.Instance.unpressedMat;
            CD = false;
        }
    }

    public class Rewind : MonoBehaviour
    {
        public bool CD = false;

        public void Awake() => transform.GetComponent<Renderer>().material = Plugin.Instance.unpressedMat;

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.TryGetComponent<GorillaTriggerColliderHandIndicator>(out GorillaTriggerColliderHandIndicator component) && Time.time > Plugin.Instance.LastPress + Plugin.Debounce)
            {
                if (!Plugin.Instance.CD)
                {
                    if (!CD)
                    {
                        Plugin.Instance.LastPress = Time.time;

                        StartCoroutine(Press());
                        GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(67, component.isLeftHand, 0.05f);
                        GorillaTagger.Instance.StartVibration(component.isLeftHand, GorillaTagger.Instance.tapHapticStrength / 1, GorillaTagger.Instance.tapHapticDuration / 1);
                    }
                }
            }
        }

        private IEnumerator Press()
        {
            CD = true;
            transform.GetComponent<Renderer>().material = Plugin.Instance.pressedMat;
            Plugin.Instance.RewindPress();
            yield return new WaitForSeconds(0.25f);
            transform.GetComponent<Renderer>().material = Plugin.Instance.unpressedMat;
            CD = false;
        }
    }

    public class Play : MonoBehaviour
    {
        public bool CD = false;

        public void Awake() => transform.GetComponent<Renderer>().material = Plugin.Instance.unpressedMat;

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.TryGetComponent<GorillaTriggerColliderHandIndicator>(out GorillaTriggerColliderHandIndicator component) && Time.time > Plugin.Instance.LastPress + Plugin.Debounce)
            {
                if (!Plugin.Instance.CD)
                {
                    if (!CD)
                    {
                        Plugin.Instance.LastPress = Time.time;

                        StartCoroutine(Press());
                        GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(67, component.isLeftHand, 0.05f);
                        GorillaTagger.Instance.StartVibration(component.isLeftHand, GorillaTagger.Instance.tapHapticStrength / 1, GorillaTagger.Instance.tapHapticDuration / 1);
                    }
                }
            }
        }

        private IEnumerator Press()
        {
            CD = true;
            transform.GetComponent<Renderer>().material = Plugin.Instance.pressedMat;
            Plugin.Instance.Play();
            yield return new WaitForSeconds(0.25f);
            transform.GetComponent<Renderer>().material = Plugin.Instance.unpressedMat;
            CD = false;
        }
    }

    public class Pause : MonoBehaviour
    {
        public bool CD = false;

        public void Awake() => transform.GetComponent<Renderer>().material = Plugin.Instance.unpressedMat;

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.TryGetComponent<GorillaTriggerColliderHandIndicator>(out GorillaTriggerColliderHandIndicator component) && Time.time > Plugin.Instance.LastPress + Plugin.Debounce)
            {
                if (!Plugin.Instance.CD)
                {
                    if (!CD)
                    {
                        Plugin.Instance.LastPress = Time.time;

                        StartCoroutine(Press());
                        GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(67, component.isLeftHand, 0.05f);
                        GorillaTagger.Instance.StartVibration(component.isLeftHand, GorillaTagger.Instance.tapHapticStrength / 1, GorillaTagger.Instance.tapHapticDuration / 1);
                    }
                }
            }
        }

        private IEnumerator Press()
        {
            CD = true;
            transform.GetComponent<Renderer>().material = Plugin.Instance.pressedMat;
            Plugin.Instance.Pause();
            yield return new WaitForSeconds(0.25f);
            transform.GetComponent<Renderer>().material = Plugin.Instance.unpressedMat;
            CD = false;
        }
    }

    public class Backward : MonoBehaviour
    {
        public bool CD = false;

        public void Awake() => transform.GetComponent<Renderer>().material = Plugin.Instance.unpressedMat;

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.TryGetComponent<GorillaTriggerColliderHandIndicator>(out GorillaTriggerColliderHandIndicator component) && Time.time > Plugin.Instance.LastPress + Plugin.Debounce)
            {
                if (!Plugin.Instance.CD)
                {
                    if (!CD)
                    {
                        Plugin.Instance.LastPress = Time.time;

                        StartCoroutine(Press());
                        GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(67, component.isLeftHand, 0.05f);
                        GorillaTagger.Instance.StartVibration(component.isLeftHand, GorillaTagger.Instance.tapHapticStrength / 1, GorillaTagger.Instance.tapHapticDuration / 1);
                    }
                }
            }
        }

        private IEnumerator Press()
        {
            CD = true;
            transform.GetComponent<Renderer>().material = Plugin.Instance.pressedMat;
            Plugin.Instance.Backward();
            Plugin.Instance.VButton1.GetComponent<Renderer>().material = Plugin.Instance.unpressedMat;
            Plugin.Instance.VButton2.GetComponent<Renderer>().material = Plugin.Instance.unpressedMat;
            Plugin.Instance.VButton3.GetComponent<Renderer>().material = Plugin.Instance.unpressedMat;
            Plugin.Instance.VButton4.GetComponent<Renderer>().material = Plugin.Instance.unpressedMat;
            Plugin.Instance.VButton5.GetComponent<Renderer>().material = Plugin.Instance.unpressedMat;
            yield return new WaitForSeconds(0.25f);
            transform.GetComponent<Renderer>().material = Plugin.Instance.unpressedMat;
            CD = false;
        }
    }

    public class YTlogo : MonoBehaviour
    {
        public bool CD = false;

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.TryGetComponent<GorillaTriggerColliderHandIndicator>(out GorillaTriggerColliderHandIndicator component) && Time.time > Plugin.Instance.LastPress + Plugin.Debounce)
            {
                if (!CD)
                {
                    Plugin.Instance.LastPress = Time.time;

                    StartCoroutine(Press());
                    GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(67, component.isLeftHand, 0.05f);
                    GorillaTagger.Instance.StartVibration(component.isLeftHand, GorillaTagger.Instance.tapHapticStrength / 1, GorillaTagger.Instance.tapHapticDuration / 1);
                }
            }
        }

        private IEnumerator Press()
        {
            CD = true;
            Plugin.Instance.VButton1.GetComponent<Renderer>().material = Plugin.Instance.unpressedMat;
            Plugin.Instance.VButton2.GetComponent<Renderer>().material = Plugin.Instance.unpressedMat;
            Plugin.Instance.VButton3.GetComponent<Renderer>().material = Plugin.Instance.unpressedMat;
            Plugin.Instance.VButton4.GetComponent<Renderer>().material = Plugin.Instance.unpressedMat;
            Plugin.Instance.VButton5.GetComponent<Renderer>().material = Plugin.Instance.unpressedMat;
            Plugin.Instance.YTHit();
            yield return new WaitForSeconds(0.25f);
            CD = false;
        }
    }
}
