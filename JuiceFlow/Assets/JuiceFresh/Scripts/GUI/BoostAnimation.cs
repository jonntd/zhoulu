using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoostAnimation : MonoBehaviour
{
    public Square square;
    public GameObject appearingEffect;

    public void ShowEffect()
    {
        GameObject partcl = Instantiate(Resources.Load("Prefabs/Effects/Firework"), transform.position, Quaternion.identity) as GameObject;
        if (square.item)
            partcl.GetComponent<ParticleSystem>().startColor = LevelManager.THIS.scoresColors[square.item.color];
        Destroy(partcl, 1f);

    }


    public void OnFinished(BoostType boostType)
    {
        GameObject effect = Instantiate(appearingEffect) as GameObject;
        effect.transform.position = transform.position;
        Destroy(effect, 2);

        if (LevelManager.THIS.waitingBoost.type == BoostType.Bomb)
        {
            LevelManager.THIS.waitingBoost = null;
            List<Square> itemsList = LevelManager.THIS.GetSquaresAround(square);
            foreach (Square _square in itemsList)
            {

                if (_square.item != null)
                {
                    /*if (_square.item.currentType != ItemsTypes.CHOCOBOMB && _square.item.currentType != ItemsTypes.INGREDIENT && _square.CheckDamage(5))
                        _square.item.DestroyItem(true, "destroy_package");*/


                    if (_square.item.currentType != ItemsTypes.CHOCOBOMB && _square.item.currentType != ItemsTypes.INGREDIENT && _square.square.CheckDamage(5))
                    {
                        if (_square.item.currentType == ItemsTypes.HORIZONTAL_STRIPPED)
                        {
                            _square.item.extraChecked = true;
                            _square.item.DestroyVertical();
                        }
                        else if (_square.item.currentType == ItemsTypes.VERTICAL_STRIPPED)
                        {
                            _square.item.extraChecked = true;
                            _square.item.DestroyHorizontal();
                        }
                        else
                        {
                            _square.item.DestroyItem(true, "destroy_package");
                        }
                    }

                }
                if (_square.IsHaveDestroybleObstacle())
                {
                    _square.DestroyBlock();
                }
            }
        }
        else if (LevelManager.THIS.waitingBoost.type == BoostType.Shovel)
        {
            LevelManager.THIS.waitingBoost = null;
            if (square.CheckDamage(5) && square.item)
                square.item.DestroyItem(false, "", false, true);
            if (square.IsHaveDestroybleObstacle())
            {
                square.DestroyBlock();
            }
        }
        else if (LevelManager.THIS.waitingBoost.type == BoostType.Energy)
        {
            LevelManager.THIS.waitingBoost = null;

            if (!square.item)
            {
                Item item = square.GenItem(false);
                if (item != null)
                {
                    item.transform.position = square.transform.position;
                    item.sprRenderer.enabled = false;
                }
            }
            if (square.item != null)
            {
                square.item.DestroyVertical(true);
                if (square.item != null)
                    square.item.DestroyHorizontal(true);
            }


            if (square.item != null && square.CheckDamage(5))
                square.item.DestroyItem(false, "", false, true);

        }
        LevelManager.THIS.StartCoroutine(LevelManager.THIS.FindMatchDelay());
        if (!name.Contains("shovel"))
            SoundBase.Instance.PlaySound(SoundBase.Instance.explosion);
        else
            SoundBase.Instance.PlaySound(SoundBase.Instance.shovel);

        LevelManager.THIS.ClearHighlight(true);
        Destroy(gameObject);
    }
}
