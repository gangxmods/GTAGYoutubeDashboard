using System.Collections;
using UnityEngine;

namespace YoutubeDashboard
{

    public class VButton1: GorillaPressableButton
    {
        public Material pressedMat;
        public Material unpressedMat;

        public bool CD = false;

        public void Awake()
        {
            pressedMat = new Material(Shader.Find("GorillaTag/UberShader")) { color = Color.red };
            unpressedMat = new Material(Shader.Find("GorillaTag/UberShader")) { color = new Color(164f, 158f, 158f, 255f) };
            transform.GetComponent<Renderer>().material = this.unpressedMat;
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (!Plugin.CD)
            {
                if (!CD)
                {
                    StartCoroutine(Press());
                }
            }
        }

        private IEnumerator Press()
        {
            CD = true;
            transform.GetComponent<Renderer>().material = this.pressedMat;
            Plugin.VideoButton1();
            yield return new WaitForSeconds(0.25f);
            transform.GetComponent<Renderer>().material = this.unpressedMat;
            CD = false;
        }
    }

    public class VButton2 : GorillaPressableButton
    {
        public Material pressedMat;
        public Material unpressedMat;

        public bool CD = false;

        public void Awake()
        {
            pressedMat = new Material(Shader.Find("GorillaTag/UberShader")) { color = Color.red };
            unpressedMat = new Material(Shader.Find("GorillaTag/UberShader")) { color = new Color(164f, 158f, 158f, 255f) };
            transform.GetComponent<Renderer>().material = this.unpressedMat;
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (!Plugin.CD)
            {
                if (!CD)
                {
                    StartCoroutine(Press());
                }
            }
        }

        private IEnumerator Press()
        {
            CD = true;
            transform.GetComponent<Renderer>().material = this.pressedMat;
            Plugin.VideoButton2();
            yield return new WaitForSeconds(0.25f);
            transform.GetComponent<Renderer>().material = this.unpressedMat;
            CD = false;
        }
    }

    public class VButton3 : GorillaPressableButton
    {
        public Material pressedMat;
        public Material unpressedMat;

        public bool CD = false;

        public void Awake()
        {
            pressedMat = new Material(Shader.Find("GorillaTag/UberShader")) { color = Color.red };
            unpressedMat = new Material(Shader.Find("GorillaTag/UberShader")) { color = new Color(164f, 158f, 158f, 255f) };
            transform.GetComponent<Renderer>().material = this.unpressedMat;
        }
        private void OnTriggerEnter(Collider collider)
        {
            if (!Plugin.CD)
            {
                if (!CD)
                {
                    StartCoroutine(Press());
                }
            }
        }

        private IEnumerator Press()
        {
            CD = true;
            transform.GetComponent<Renderer>().material = this.pressedMat;
            Plugin.VideoButton3();
            yield return new WaitForSeconds(0.25f);
            transform.GetComponent<Renderer>().material = this.unpressedMat;
            CD = false;
        }
    }

    public class VButton4 : GorillaPressableButton
    {
        public Material pressedMat;
        public Material unpressedMat;

        public bool CD = false;

        public void Awake()
        {
            pressedMat = new Material(Shader.Find("GorillaTag/UberShader")) { color = Color.red };
            unpressedMat = new Material(Shader.Find("GorillaTag/UberShader")) { color = new Color(164f, 158f, 158f, 255f) };
            transform.GetComponent<Renderer>().material = this.unpressedMat;
        }
        private void OnTriggerEnter(Collider collider)
        {
            if (!Plugin.CD)
            {
                if (!CD)
                {
                    StartCoroutine(Press());
                }
            }
        }

        private IEnumerator Press()
        {
            CD = true;
            transform.GetComponent<Renderer>().material = this.pressedMat;
            Plugin.VideoButton4();
            yield return new WaitForSeconds(0.25f);
            transform.GetComponent<Renderer>().material = this.unpressedMat;
            CD = false;
        }
    }
    public class VButton5 : GorillaPressableButton
    {
        public Material pressedMat;
        public Material unpressedMat;

        public bool CD = false;

        public void Awake()
        {
            pressedMat = new Material(Shader.Find("GorillaTag/UberShader")) { color = Color.red };
            unpressedMat = new Material(Shader.Find("GorillaTag/UberShader")) { color = new Color(164f, 158f, 158f, 255f) };
            transform.GetComponent<Renderer>().material = this.unpressedMat;
        }
        private void OnTriggerEnter(Collider collider)
        {
            if (!Plugin.CD)
            {
                if (!CD)
                {
                    StartCoroutine(Press());
                }
            }
        }

        private IEnumerator Press()
        {
            CD = true;
            transform.GetComponent<Renderer>().material = this.pressedMat;
            Plugin.VideoButton5();
            yield return new WaitForSeconds(0.25f);
            transform.GetComponent<Renderer>().material = this.unpressedMat;
            CD = false;
        }
    }

    public class Forward : GorillaPressableButton
    {
        public Material pressedMat;
        public Material unpressedMat;

        public bool CD = false;

        public void Awake()
        {
            pressedMat = new Material(Shader.Find("GorillaTag/UberShader")) { color = Color.red };
            unpressedMat = new Material(Shader.Find("GorillaTag/UberShader")) { color = new Color(164f, 158f, 158f, 255f) };
            transform.GetComponent<Renderer>().material = this.unpressedMat;
        }
        private void OnTriggerEnter(Collider collider)
        {
                if (!CD)
                {
                    StartCoroutine(Press());
                }           
        }

        private IEnumerator Press()
        {
            CD = true;
            transform.GetComponent<Renderer>().material = this.pressedMat;
            Plugin.Forward();
            Plugin.VButton1.GetComponent<Renderer>().material = unpressedMat;
            Plugin.VButton2.GetComponent<Renderer>().material = unpressedMat;
            Plugin.VButton3.GetComponent<Renderer>().material = unpressedMat;
            Plugin.VButton4.GetComponent<Renderer>().material = unpressedMat;
            Plugin.VButton5.GetComponent<Renderer>().material = unpressedMat;
            yield return new WaitForSeconds(0.25f);
            transform.GetComponent<Renderer>().material = this.unpressedMat;
            CD = false;
        }
    }
    public class Mute : GorillaPressableButton
    {
        public Material pressedMat;
        public Material unpressedMat;

        public bool CD = false;

        public void Awake()
        {
            pressedMat = new Material(Shader.Find("GorillaTag/UberShader")) { color = Color.red };
            unpressedMat = new Material(Shader.Find("GorillaTag/UberShader")) { color = new Color(164f, 158f, 158f, 255f) };
            transform.GetComponent<Renderer>().material = this.unpressedMat;
        }
        private void OnTriggerEnter(Collider collider)
        {
            if (!Plugin.CD)
            {
                if (!CD)
                {
                    StartCoroutine(Press());
                }
            }
        }

        private IEnumerator Press()
        {
            CD = true;
            transform.GetComponent<Renderer>().material = this.pressedMat;
            Plugin.Mute();
            yield return new WaitForSeconds(0.25f);
            transform.GetComponent<Renderer>().material = this.unpressedMat;
            CD = false;
        }
    }

    public class Play : GorillaPressableButton
    {
        public Material pressedMat;
        public Material unpressedMat;

        public bool CD = false;

        public void Awake()
        {
            pressedMat = new Material(Shader.Find("GorillaTag/UberShader")) { color = Color.red };
            unpressedMat = new Material(Shader.Find("GorillaTag/UberShader")) { color = new Color(164f, 158f, 158f, 255f) };
            transform.GetComponent<Renderer>().material = this.unpressedMat;
        }
        private void OnTriggerEnter(Collider collider)
        {
            if (!Plugin.CD)
            {
                if (!CD)
                {
                    StartCoroutine(Press());
                }
            }
        }

        private IEnumerator Press()
        {
            CD = true;
            transform.GetComponent<Renderer>().material = this.pressedMat;
            Plugin.Play();
            yield return new WaitForSeconds(0.25f);
            transform.GetComponent<Renderer>().material = this.unpressedMat;
            CD = false;
        }
    }

    public class Pause : GorillaPressableButton
    {
        public Material pressedMat;
        public Material unpressedMat;

        public bool CD = false;

        public void Awake()
        {
            pressedMat = new Material(Shader.Find("GorillaTag/UberShader")) { color = Color.red };
            unpressedMat = new Material(Shader.Find("GorillaTag/UberShader")) { color = new Color(164f, 158f, 158f, 255f) };
            transform.GetComponent<Renderer>().material = this.unpressedMat;
        }
        private void OnTriggerEnter(Collider collider)
        {
            if (!CD)
            {
                StartCoroutine(Press());
            }       
        }

        private IEnumerator Press()
        {
            CD = true;
            transform.GetComponent<Renderer>().material = this.pressedMat;
            Plugin.Pause();
            yield return new WaitForSeconds(0.25f);
            transform.GetComponent<Renderer>().material = this.unpressedMat;
            CD = false;
        }
    }

    public class Backward : GorillaPressableButton
    {
        public Material pressedMat;
        public Material unpressedMat;

        public bool CD = false;

        public void Awake()
        {
            pressedMat = new Material(Shader.Find("GorillaTag/UberShader")) { color = Color.red };
            unpressedMat = new Material(Shader.Find("GorillaTag/UberShader")) { color = new Color(164f, 158f, 158f, 255f) };
            transform.GetComponent<Renderer>().material = this.unpressedMat;
        }
        private void OnTriggerEnter(Collider collider)
        {
            if (!CD)
            {
                StartCoroutine(Press());
            }    
        }

        private IEnumerator Press()
        {
            CD = true;
            transform.GetComponent<Renderer>().material = this.pressedMat;
            Plugin.Backward();
            Plugin.VButton1.GetComponent<Renderer>().material = unpressedMat;
            Plugin.VButton2.GetComponent<Renderer>().material = unpressedMat;
            Plugin.VButton3.GetComponent<Renderer>().material = unpressedMat;
            Plugin.VButton4.GetComponent<Renderer>().material = unpressedMat;
            Plugin.VButton5.GetComponent<Renderer>().material = unpressedMat;
            yield return new WaitForSeconds(0.25f);
            transform.GetComponent<Renderer>().material = this.unpressedMat;
            CD = false;
        }
    }

    public class YTlogo : GorillaPressableButton
    {
        public Material unpressedMat;
        public bool CD = false;

        public void Awake()
        {
            unpressedMat = new Material(Shader.Find("GorillaTag/UberShader")) { color = new Color(164f, 158f, 158f, 255f) };
        }
        private void OnTriggerEnter(Collider collider)
        {
            if (!CD)
            {
                StartCoroutine(Press());
            }          
        }

        private IEnumerator Press()
        {
            CD = true;
            Plugin.VButton1.GetComponent<Renderer>().material = unpressedMat;
            Plugin.VButton2.GetComponent<Renderer>().material = unpressedMat;
            Plugin.VButton3.GetComponent<Renderer>().material = unpressedMat;
            Plugin.VButton4.GetComponent<Renderer>().material = unpressedMat;
            Plugin.VButton5.GetComponent<Renderer>().material = unpressedMat;
            Plugin.YTHit();
            yield return new WaitForSeconds(0.25f);
            CD = false;
        }
    }


}
