using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GSS.Inventory;
using GSS.Combat;
using TMPro;

public class CheckoutSys : MonoBehaviour
{
    // public GenericItem itemNOTHING;
    [SerializeField] DragDropManager DDM;
    public static CheckoutSys instance;
    public List<PanelSettings> ChkoutList;
    public List<GenericItem> chkoutItems = new List<GenericItem>();

    public int itemCount;
    public float chkoutCost;
    public ObjectSettings itemRemovedFromChkout;
    public TextMeshProUGUI txtChkoutTotal;

    public static CheckoutSys main;

    void Awake(){
        if(main!=null&&main!=this)
            Destroy(this.gameObject);
        else
        { main = this; }
        // GenericItem.NOTHING = itemNOTHING;
    }


    public void fncAddToChkout(PanelSettings thisSlot)
    {
        // if (thisSlot.curItem!=null)
        //     foreach (PanelSettings findSlot in ChkoutList)
        //     {
        //         if (findSlot.counterId == thisSlot.counterId) chkoutItems.Remove(thisSlot.curItem);
        //     }
        // chkoutItems.Add(thisSlot.curItem);
        // fncCalcChkout(thisSlot, true);
        fncCalcChkout(thisSlot, true);
    }
    public void fncRemToChkout(ObjectSettings thisItem)
    {
        // foreach (PanelSettings thisSlot in ChkoutList)
        // {
        //     if (thisSlot.curItem == null) continue;
        //     {
        //         if (thisSlot.curItem.objectSettings.Id == thisItem.Id)
        //         {
        //             chkoutItems.Remove(thisSlot.curItem);
        //             fncCalcChkout(thisSlot,false);
        //             itemRemovedFromChkout = thisItem;
        //         }
        //     }
        // }
        // if (ChkoutList.Contains(thisSlot))
        int getIndex = 0; string getID = DragDropManager.GetObjectPanel(thisItem.Id);
        bool isChkout = false;
        foreach (PanelSettings thisSlot in ChkoutList)
        {
            isChkout = thisSlot.id == thisItem.Id;
            if (isChkout) break;
        }
        for (int i = 0; i < DDM.AllPanels.Length; i++)
        { if (DDM.AllPanels[i].id == thisItem.Id) getIndex = i; }
        fncCalcChkout(DDM.AllPanels[getIndex], false);
    }
    public void fncReturnToChkout(ObjectSettings thisItem)
    {
        // foreach (PanelSettings thisSlot in ChkoutList)
        // {
        //     if (thisSlot.curItem == null) continue;
        //     if (thisSlot.curItem.objectSettings.Id == itemRemovedFromChkout.Id)
        //     {
        //         fncAddToChkout(thisSlot);
        //         // fncCalcChkout(thisSlot, true);
        //         //fncCalcChkout(thisSlot, false);
        //     }
        // }
        // if (ChkoutList.Contains(thisSlot))
            fncCalcChkout(ChkoutList[0], true);
    }

    public void fncCalcChkout(PanelSettings thisSlot, bool isAdd)
    {
        #region 
        //checks each chkout slots
        // float newCost = 0;
        // int newCount = 0;
        // // string itemID;
        // foreach (PanelSettings curSlot in ChkoutList)
        // {
        //     // itemID = DragDropManager.GetPanelObject(thisSlot.counterId);
        //     if (curSlot.curItem != null)
        //     {
        //         // foreach (GSS.Inventory.GenericItem item in thisSlot.itemController.genericItems)
        //         // {
        //         // if (item.id == itemID)

        //         newCost += (curSlot.id == thisSlot.id) ? (isAdd ? curSlot.curItem.price : -curSlot.curItem.price) : curSlot.curItem.price;
        //         print($"//CHKOUT: Adding price of {thisSlot.curItem.price} Thaler of item, {thisSlot.curItem.objectSettings.Id}, from {thisSlot.id}");

        //         newCount += isAdd ? 1 : -1;
        //     }
        //     else { continue; }
        // }
        // itemCount = newCount;
        // chkoutCost = newCost;
        #endregion
        float newCost = 0;//chkoutCost;
                          //newCost += isAdd ? thisSlot.curItem.price : -thisSlot.curItem.price;
                          // print($"//CHKOUT: Adding price of {thisSlot.curItem.price} Thaler of item, {thisSlot.curItem.objectSettings.Id}, from {thisSlot.id}");

        // foreach (GenericItem thisItem in chkoutItems)
        // {
        //     newCost += thisItem.price;
        //     print($"//CHKOUT: Adding price of {thisSlot.curItem.price} Thaler of item, {thisSlot.curItem.objectSettings.Id}, from {thisSlot.id}");
        // }
        chkoutItems.Clear();
        int getIndex = 0; string getID = "";
        foreach (PanelSettings checkSlot in ChkoutList)
        {

            getID = DragDropManager.GetPanelObject(checkSlot.id);
            if (string.IsNullOrEmpty(getID))//(getID == "") continue;
            {
                // print($"Slot {checkSlot.id} is empty!");
                continue;
            }
            for (int i = 0; i < DDM.AllObjects.Length; i++)
            {
                if (DDM.AllObjects[i].Id == getID) getIndex = i;
            }
            chkoutItems.Add(DDM.AllObjects[getIndex].itemData);
            if ((checkSlot.id == thisSlot.id) && !isAdd) continue;
            newCost += DDM.AllObjects[getIndex].itemData.price;
        }
        chkoutCost = newCost;
        chkoutCost = (newCost < 0 ? 0 : newCost);
        txtChkoutTotal.text = $"{chkoutCost}G";

    }

    public void fncCheckEquippable(PanelSettings thisSlot)
    {
        // if (!CharacterGenerator.instance.GetSelected().isEquippable(DDM?.curObj.itemData.item))
        // {
        //     thisSlot.GetComponent<UnityEngine.UI.Image>().raycastTarget=false;
        //     print($"//CHECKOUTSYS: This item, {DDM.curObj.itemData.id}, cannot be equipped by this character, {CharacterGenerator.instance.GetSelected().name}! ");
        // }
    }
    public void fncReActiviateSlot(PanelSettings thisSlot)
    {
        // thisSlot.GetComponent<UnityEngine.UI.Image>().raycastTarget=false;
        // print($"//CHECKOUTSYS: Slot {thisSlot.id}, is now reactivated.");
        }

    public bool fncChkoutCompat()//Checks if the items in the checkout is compatible with character
    {
        
        foreach (GenericItem chkItem in chkoutItems)
        {
            if (!CharacterGenerator.instance.GetSelected().isEquippable(chkItem.item))
            {
                print($"//CHKOUT: Checkout contains incompatible item, {chkItem.id}");
                return false;
            }
        }
        print($"//CHKOUT: Checkout contains no incompatible items");
        return true;
    }

    public void fncCheckoutItems()//Checksout the items and applies its effect
    {
        // int curInd = GSS.Combat.CharacterGenerator.instance.id;
        // string reportProgress =
        int getHealthMod = 0, getDefMod = 0, getAtkMod = 0, getTotalCost = 0;
        print($"//CHKOUTSYS: Character {CharacterGenerator.instance.GetSelected().name} before has " + fncReportStat());
        foreach (GenericItem curItem in chkoutItems)
        {
            getHealthMod += curItem.hp;
            getDefMod += curItem.def;
            getAtkMod += curItem.atk;
            getTotalCost += Mathf.RoundToInt(curItem.price);
        }
        // CharacterGenerator.instance.GetSelected().healt}
        CharacterGenerator.instance.GetSelected().health += getHealthMod;//curItem.hp;
        CharacterGenerator.instance.GetSelected().defense += getDefMod;//curItem.def;
        CharacterGenerator.instance.GetSelected().attack += getAtkMod;//curItem.atk;
        CharacterGenerator.instance.GetSelected().gold -= getTotalCost;//Mathf.RoundToInt(curItem.price);
        print($"//CHKOUTSYS: Character {CharacterGenerator.instance.GetSelected().name} now has " + fncReportStat());
        // GSS.Combat.CharacterGenerator.instance.characterStats // GSS.Combat.CharacterGenerator.instance.GetSelected;

        // StartCoroutine(fncClearCounter());
        chkoutItems.Clear();
    }

    public List<PanelSettings> returnWpn, returnItem,returnOccupied;

    
    IEnumerator fncClearCounter()
    {//more like object pooling
        // List<int> returnIDs=new List<int>();
        returnOccupied.Clear();
        float oldAISpd = DDM.GetComponent<AIManager>().MovementSpeed; 
        DDM.GetComponent<AIManager>().MovementSpeed= 42;
        // for (int i = 0; i < DDM.AllPanels.Length; i++)
        // {
        //     string getObjID = DragDropManager.GetObjectPanel(DDM.AllPanels[i].id);
        //     if (string.IsNullOrEmpty(getObjID)) continue;
        //     DDM.AllObjects[i].itemData.image.enabled = false;
        //     print($"CHKOUT: PRIMING {i}");
        //     DDM.AllObjects[i].AIControl = true;
        //     DDM.AllObjects[i].MoveSmoothly = false;
        //     AIManager.AIDragDrop(getObjID, DDM.AllPanels[i].id);
        //     // returnOccupied.Add(DDM.AllPanels[i]);
        //     // AIManager.AIDragDrop(DDM.AllObjects[i].Id, getObjID);
        // }
        List<string> occupiedID = new List<string>();
        string getSlot;
        for (int i = 0; i < DDM.AllObjects.Length; i++)
        {
            DDM.AllObjects[i].AIControl = true;
            DDM.AllObjects[i].MoveSmoothly = false;
            getSlot = DragDropManager.GetObjectPanel(DDM.AllObjects[i].Id);
            for (int j = 0; j < DDM.AllPanels.Length; j++)
            {
                if (DDM.AllPanels[j].id == getSlot)
                {
                    returnOccupied.Add(DDM.AllPanels[j]);
                    occupiedID.Add(DDM.AllPanels[j].id);
                    if(occupiedID[occupiedID.Count-1]=="1")
                        print($"Start of row, {occupiedID}");
                    AIManager.AIDragDrop(DDM.AllObjects[i].Id, DDM.AllPanels[j].id);
                }
            }
        }
        yield return new WaitForFixedUpdate();
        foreach (PanelSettings thisSlot in ChkoutList)
        {
            // thisSlot.curItem.image.enabled = false;
            // if (thisSlot.curItem != null)
            // {
            string getID = DragDropManager.GetPanelObject(thisSlot.id);
            if (string.IsNullOrEmpty(getID)) continue;//(getID == "") continue;
            int getIndex = 0;
            for (int i = 0; i < DDM.AllObjects.Length; i++)
            {
                if (DDM.AllObjects[i].Id == getID)
                    getIndex = i;
            }
            // returnIDs.Add(getIndex);
            DDM.AllObjects[getIndex].itemData.image.enabled = false;
            DDM.AllObjects[getIndex].MoveSmoothly = false;
            DDM.AllObjects[getIndex].AIControl = true;

            //
            // bool isDone = false;
            foreach (PanelSettings chkSlot in DDM.AllObjects[getIndex].itemData.itemType == ItemType.Resources ? returnItem : returnWpn)
            {
                // bool isOccupied = false;
                // foreach (PanelSettings compare in returnOccupied)
                // {
                //     isOccupied = compare.id == chkSlot.id;
                //     if (isOccupied) break;
                // }
                if (!occupiedID.Contains(chkSlot.id))//(!returnOccupied.Contains(chkSlot))
                {
                    AIManager.AIDragDrop(DDM.AllObjects[getIndex].Id, chkSlot.id);
                    returnOccupied.Add(chkSlot);
                    occupiedID.Add(chkSlot.id);
                    print($"//CHECKOUTSYS, moving {DDM.AllObjects[getIndex].Id} to {chkSlot.id}");
                    
                    break;
                }
                else
                print($"//CHECKOUTSYS, slot {chkSlot.id} is"+  (occupiedID.Contains(chkSlot.id)?" occupied":"not occupied"));
                // else
                // print($"//CHECKOUTSYS, slot {chkSlot.id} is occupied.");
                // if(isDone)break;
            }
            #region Old Code
            // if (DDM.AllObjects[getIndex].itemData.itemType == ItemType.Resources)//thisSlot.curItem.itemType == ItemType.Resources)
            // {
            //     foreach (PanelSettings chkSlot in DDM.AllObjects[getIndex].itemData.itemType==ItemType.Resources? returnItem:returnWpn)
            //     {
            //         // AIManager.AIDragDrop(DragDropManager.GetPanelObject(chkSlot.id), chkSlot.id);
            //         // print($"//DIAGNOSYS: isEmpty: {string.IsNullOrEmpty(getID)}");
            //         // if (!string.IsNullOrEmpty(DragDropManager.GetPanelObject(chkSlot.id)))
            //         // {
            //         //      print($"//CHECKOUTSYS, {chkSlot.id} is occupied");
            //         //     continue;
            //         // }
            //         // if (string.IsNullOrEmpty(DragDropManager.GetPanelObject(chkSlot.id))&& !returnOccupied.Contains(chkSlot))//(chkSlot.curItem == null)
            //         // {
            //         //     // foreach(PanelSettings search in returnOccupied)
            //         //         AIManager.AIDragDrop(DDM.AllObjects[getIndex].Id, chkSlot.id);
            //         //     returnOccupied.Add(chkSlot);
            //         //     print($"//CHECKOUTSYS, moving {DDM.AllObjects[getIndex].Id} to {chkSlot.id}");
            //         // }
            //         for (int i = 0; i < DDM.AllObjects.Length;i++){
            //             if()
            //             if (DragDropManager.GetObjectPanel(DDM.AllObjects[i].Id) == DDM.AllObjects[i].Id)
            //             {
            //                 print($"//CHECKSYS, slot {chkSlot.id} is occupied");
            //                 break;
            //             }
            //         }

            //     }
            // }
            // else if (DDM.AllObjects[getIndex].itemData.itemType == ItemType.Weapons)//thisSlot.curItem.itemType == ItemType.Weapons)
            // {
            //     foreach (PanelSettings chkSlot in returnWpn)
            //     {
            //         // AIManager.AIDragDrop(DragDropManager.GetPanelObject(chkSlot.id), chkSlot.id);
            //         //print($"//DIAGNOSYS: isEmpty: {string.IsNullOrEmpty(getID)}");
            //         // if (DragDropManager.GetPanelObject(chkSlot.id) == "" && !returnOccupied.Contains(chkSlot))//(chkSlot.curItem == null)
            //         if (!string.IsNullOrEmpty(DragDropManager.GetPanelObject(chkSlot.id)))
            //         {
            //              print($"//CHECKOUTSYS, {chkSlot.id} is occupied");
            //             continue;
            //         }
            //         if (string.IsNullOrEmpty(DragDropManager.GetPanelObject(chkSlot.id)) && !returnOccupied.Contains(chkSlot))
            //         {
            //             AIManager.AIDragDrop(DDM.AllObjects[getIndex].Id, chkSlot.id);
            //             returnOccupied.Add(chkSlot);
            //             print($"//CHECKOUTSYS, moving {DDM.AllObjects[getIndex].Id} to {chkSlot.id}");
            //         }
            //     }
            // }
            // print($"//CHECKOUTSYS, moving {DDM.AllObjects[getIndex].Id}");
            //             string getID = DragDropManager.GetPanelObject(thisSlot.id);
            //             int getIndex = (int)(getID[getID.Length - 1]);
            //  AIManager.AIDragDrop(getID, thisslo
            #endregion
        }
        // foreach(PanelSettings chkoutitem in ChkoutList)
        yield return new WaitForSeconds(.2f);
        DDM.GetComponent<AIManager>().MovementSpeed = oldAISpd;
        for (int i = 0; i < DDM.AllObjects.Length;i++)// foreach (int thisID in returnIDs)
        {
            DDM.AllObjects[i].MoveSmoothly = true;
            DDM.AllObjects[i].AIControl = false;
            DDM.AllObjects[i].itemData.image.enabled = true;
        }
        // foreach (PanelSettings thisSlot in ChkoutList)
        // {
        //     thisSlot.curItem.objectSettings.MoveSmoothly = true;
        //     // thisSlot.curItem.image.enabled = true;
        // }

    }

    public string fncReportStat(){
        return $"a health of {CharacterGenerator.instance.GetSelected().health}, " +
        $"an attack of {CharacterGenerator.instance.GetSelected().attack}," +
        $"a defense of {CharacterGenerator.instance.GetSelected().defense}." +
        $"and a purse of {CharacterGenerator.instance.GetSelected().gold} gold.";
    }
}