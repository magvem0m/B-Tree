BTree<string> bTree = new(); 

bTree.Add("5");
bTree.Add("4");
bTree.Add("6");
bTree.Add("5");
bTree.Add("7");
bTree.Add("9");
bTree.Add("8");
bTree.Add("0");
bTree.Add("2");
bTree.Add("-1");

bTree.BreadthFirst();
bTree.DepthFirst();

class BTree<T> where T: IComparable
{
    private BTreeNode? Root;

    public void Add(T value)
    {
        if (Root == null)
        {
            Root = new BTreeNode(value, null);
            return;
        }

        Add(value, Root);
    }

    private void Add(T value, BTreeNode currentNode)
    {
        int compared = value.CompareTo(currentNode.Value);

        switch (compared)
        {
            case <= 0: if (currentNode.Left == null) currentNode.Left = new BTreeNode(value, currentNode); else Add(value, currentNode.Left); break;
            case > 0: if (currentNode.Right == null) currentNode.Right = new BTreeNode(value, currentNode); else Add(value, currentNode.Right); break;
        }
    }

    public void BreadthFirst()
    {
        if (Root == null)
            return;

        if (Root.IsList)
        {
            Console.WriteLine("Value: " + Root.Value);
            return;
        }


        Queue<BTreeNode> queue = new();
        queue.Enqueue(Root);

        while(queue.Count > 0)
        {
            BTreeNode node = queue.Dequeue();

            if (node.Left != null)
                queue.Enqueue(node.Left);

            if (node.Right != null)
                queue.Enqueue(node.Right);

            Console.WriteLine("Value: " + node.Value);
        }
    }

    public void DepthFirst()
    {
        if (Root == null)
            return;

        DepthFirst(Root);
    }

    private void DepthFirst(BTreeNode node)
    {
        Console.WriteLine("Value: " + node.Value);

        if (node.IsList)
            return;

        if(node.Left != null)
            DepthFirst(node.Left);

        if (node.Right != null)
            DepthFirst(node.Right);
    }

    private class BTreeNode
    {
        private BTreeNode? _Left, _Right;
        private readonly BTreeNode? _Parent;
        private T _Value;

        public BTreeNode(T value, BTreeNode? parent)
        {
            _Value = value;
            _Parent = parent;
        }

        public BTreeNode? Left { get => _Left; set { _Left = value; } }
        public BTreeNode? Right { get => _Right; set { _Right = value; } }
        public BTreeNode? Parent => _Parent;
        public T Value { get => _Value; set { _Value = value; } }
        public bool IsList => Left == null && Right == null;
    }
}
