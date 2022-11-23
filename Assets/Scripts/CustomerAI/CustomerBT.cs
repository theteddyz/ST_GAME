
using BehaviorTree;


public class CustomerBT : Tree
{
   public UnityEngine.Transform[] waypoints;
   public static float speed = 2f;
   protected override Node SetupTree()
   {
      Node root = new TaskOrder(transform, waypoints);
      return root;
   }
}
