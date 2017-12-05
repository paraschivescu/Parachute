using System.Collections;
using System.Collections.Generic;
using Res.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Res.Ui
{
    public class RadialProgressBar : MonoBehaviour, IRegisterProgressController
    {
        [SerializeField] private Image radialImage;
        private IProgressController controller = null;
        private float secondsInterval;
        private float progressPerTick;
        private float timeToNextTick;
        private float progressAtLastTick;
        
        
        public void SetProgress(float percentDone)
        {
            
            progressAtLastTick = percentDone;
            radialImage.fillAmount = percentDone;
            timeToNextTick = secondsInterval;
            UpdateFill(0.0f);
        }

        private void Update()
        {
            if (secondsInterval <= 0.0f)
            {
                return;
            }
            
            UpdateFill(Time.deltaTime);
        }

        private void UpdateFill(float deltaTime)
        {
            if (secondsInterval <= 0.0f)
            {    
                radialImage.fillAmount = progressAtLastTick;   
            }
            else
            {
                timeToNextTick = Mathf.Max(0.0f, timeToNextTick - deltaTime);
                var fillAmount = progressAtLastTick + progressPerTick * (1.0f - timeToNextTick / secondsInterval);
                radialImage.fillAmount = fillAmount;     
            }         
        }

        public void Register(IProgressController controller, float secondsInterval)
        {
            this.controller = controller;
            this.secondsInterval = secondsInterval;
            this.progressPerTick = this.controller.GetProgressPerTick();
            SetProgress(this.controller.GetProgress());
            this.controller.ProgressChanged.AddListener(SetProgress);
        }

        private void OnDestroy()
        {
            if (controller != null)
            {
                controller.ProgressChanged.RemoveListener(SetProgress);
            }
        }
    }
}
