using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Simulations;

namespace Simulations.Tools
{
    public class SimSCOManager : MonoBehaviour
    {
        [SerializeField] SimulationFlowSCO targetSCO;
        [SerializeField] int startRang, endRange;//use for specific updates
        [SerializeField] Vector3 scale, eulerAngles;
        [SerializeField] Sprite[] spritesToAdd;
        [SerializeField] string spriteNameIdentifier = "Sprite";//use to identify elements in later stages
        enum AvailableActions { CreateNewSpriteList,AddInExisting,UpdateExistingList}
        [SerializeField] AvailableActions curtAction;
        public void ApplyChanges()
        {
            List<SimulationFlowSCO.AnswerSpriteDataHolder> _answerSprites = new List<SimulationFlowSCO.AnswerSpriteDataHolder>();
            switch(curtAction)
            {
                case AvailableActions.CreateNewSpriteList:
                    foreach(var v in spritesToAdd)
                    {
                        _answerSprites.Add(new SimulationFlowSCO.AnswerSpriteDataHolder
                        {
                            AnswerSprite = new Sprite[] { v },
                            SpriteScale = scale,
                            SpriteEulerAngles = eulerAngles,
                            AnswerName = spriteNameIdentifier + " "+ v.name
                        }) ;
                    }
                    targetSCO.AnswerSprites = _answerSprites.ToArray();
                    break;
                case AvailableActions.AddInExisting:
                    foreach(var img in targetSCO.AnswerSprites)
                    {
                        _answerSprites.Add(img);
                    }
                    //adds existing sprites
                    foreach (var v in spritesToAdd)
                    {
                        _answerSprites.Add(new SimulationFlowSCO.AnswerSpriteDataHolder
                        {
                            AnswerSprite = new Sprite[] { v },
                            SpriteScale = scale,
                            SpriteEulerAngles = eulerAngles,
                            AnswerName = spriteNameIdentifier + " " + v.name

                        });
                    }
                    targetSCO.AnswerSprites = _answerSprites.ToArray();
                    break;
                case AvailableActions.UpdateExistingList:
                    foreach (var img in targetSCO.AnswerSprites)
                    {
                        _answerSprites.Add(img);
                    }
                    //adds existing sprites
                    for (int i = startRang,z = 0; i < endRange; i++,z++)
                    {
                        _answerSprites[i] =new SimulationFlowSCO.AnswerSpriteDataHolder
                        {
                            AnswerSprite = new Sprite[] { spritesToAdd[z] },
                            SpriteScale = scale,
                            SpriteEulerAngles = eulerAngles,
                            AnswerName = spriteNameIdentifier + " " + spritesToAdd[z].name

                        };
                    }
                    targetSCO.AnswerSprites = _answerSprites.ToArray();
                    break;
            }
            print("Successfully Applied changes to SCO: " + targetSCO.name);
        }
    }
}
