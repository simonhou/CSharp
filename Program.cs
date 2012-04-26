using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace binaryTree
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>() { 50, 30, 70, 10, 40, 90, 80 };
            //创建二叉树
            BSTree bsTree = CreateBST(list);
            Console.Write("中序遍历的原始数据");

            //中序遍历
            InOrderR_BST(bsTree);
            Console.WriteLine("\n------------------------------------------------------------n");

            //查找一个节点
            Console.WriteLine("\n10在二叉树中是否包含：" + SearchBST(bsTree, 20));
            Console.WriteLine("\n------------------------------------------------------------n");

            bool isExcute = false;

            //插入一个节点
            InsertBST(bsTree, 20, ref isExcute);
            Console.WriteLine("\n20插入到二叉树，中序遍历后：");

            //中序遍历
            InOrderR_BST(bsTree);
            Console.WriteLine("\n------------------------------------------------------------n");

            Console.Write("删除叶子节点20，\n中序遍历后：");
            //删除一个节点
            DeleteBST(ref bsTree, 20);

            InOrderR_BST(bsTree);
            Console.WriteLine("\n------------------------------------------------------------n");
            Console.WriteLine("删除单孩子节点90，\n中序遍历后：");

            //删除单孩子节点
            DeleteBST(ref bsTree, 90);
            InOrderR_BST(bsTree);
            Console.WriteLine("\n------------------------------------------------------------n");
            Console.WriteLine("删除根节点50，\n中序遍历后");
            //删除根节点
            DeleteBST(ref bsTree, 50);

            InOrderR_BST(bsTree);
        }
        //定义一个二叉树结构
        public class BSTree
        {
            public int data;
            public BSTree left;
            public BSTree right;
        }
        //二叉树的插入操做
        //ref定义一个引用传递，与C++中引用传递类似，和实际值使用同一个地址，对它的修改就是对实际值得修改
        static void InsertBST(BSTree bsTree, int key, ref bool isExcute)
        {
            if (bsTree == null)
                return;
            //如果父节点大于key,则遍历左子树
            if(bsTree.data > key)
                InsertBST(bsTree.left, key, ref isExcute);
            else
                InsertBST(bsTree.right, key, ref isExcute);

            if (!isExcute)
            {
                //构建当前节点
                BSTree current = new BSTree();
                {
                    current.data = key;
                    current.left = null;
                    current.right = null;
                };
                if (bsTree.data > key)
                    bsTree.left = current;
                else
                    bsTree.right = current;
                isExcute = true;
            }
        }

        //创建二叉树
        static BSTree CreateBST(List<int> list)
        {
            //构建根节点
            BSTree bsTree = new BSTree();
            {
                bsTree.data = list[0];
                bsTree.left = null;
                bsTree.right = null;
            };

            for (int i = 1; i < list.Count; i++)
            {
                bool isExcute = false;
                InsertBST(bsTree, list[i], ref isExcute);
            }
            return bsTree;
        }
        //在排序二叉树中搜索指定节点
        static bool SearchBST(BSTree bsTree, int key)
        {
            if (bsTree == null)
                return false;
            if (bsTree.data == key)
                return true;

            if(bsTree.data > key)
                return SearchBST(bsTree.left, key);
            else
                return SearchBST(bsTree.right, key);
        }

        //中序排列二叉树
        static void InOrderR_BST(BSTree bsTree)
        {
            if (bsTree != null)
            {
                //遍历左子树
                InOrderR_BST(bsTree.left);
                //输出节点数据
                Console.WriteLine(bsTree.data + "");
                //遍历右子书
                InOrderR_BST(bsTree.right);
            }
        }
        //删除二叉排序树中指定节点
        static void DeleteBST(ref BSTree bsTree, int key)
        {
            if (bsTree == null)
                return;

            if (bsTree.data == key)
            {
                //第一种情况：叶子节点
                if (bsTree.left == null && bsTree.right == null)
                {
                    bsTree = null;
                    return;
                }
                //第二种情况：左子树不为空
                if (bsTree.left != null && bsTree.right == null)
                {
                    bsTree = bsTree.left;
                    return;
                }
                //第三种情况：右子树不为空
                if (bsTree.left == null && bsTree.right == null)
                {
                    bsTree = bsTree.right;
                    return;

                }
                //第四种情况：左右子树都不为空
                if (bsTree.left != null && bsTree.right != null)
                {
                    var node = bsTree.right;   //
                    //找到右子树中的最左节点
                    while (node.left != null)
                    {
                        //遍历左子树
                        node = node.left;

                    }
                    //交换左右孩子
                    node.left = bsTree.left;
                    //判断是真正的叶子节点还是空左孩子的父节点
                    if (node.right == null)
                    {
                        //删掉右子树最左节点
                        DeleteBST(ref bsTree, node.data);
                        node.right = bsTree.right;
                    }
                    bsTree = node;
                }
            }
            if (bsTree.data > key)
            {
                DeleteBST(ref bsTree.left, key);
            }
            else
            {
                DeleteBST(ref bsTree.right, key);
            }
        }

    }
}
