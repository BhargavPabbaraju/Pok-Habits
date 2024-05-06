using UnityEngine.UI;
public class ScrollRect_Fix : ScrollRect {
   
     override protected void LateUpdate() {
         base.LateUpdate();
         if (this.verticalScrollbar) {
             this.verticalScrollbar.size=0f;
         }
     }
   
     override public void Rebuild(CanvasUpdate executing) {
         base.Rebuild(executing);
         if (this.verticalScrollbar) {
             this.verticalScrollbar.size=0f;
         }
     }
}