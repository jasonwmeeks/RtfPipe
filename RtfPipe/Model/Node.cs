using System;
using System.Collections.Generic;
using System.IO;

namespace RtfPipe.Model
{
  public abstract class Node
  {
    internal Node Next { get; set; }
    internal Element Parent { get; set; }

    internal void AddAfterSelf(Node node)
    {
      if (Parent == null)
        throw new InvalidOperationException("This node does not have a parent");
      Parent.InsertAfter(this, node);
    }

    internal IEnumerable<Element> Parents()
    {
      var parent = Parent;
      while (parent != null)
      {
        yield return parent;
        parent = parent.Parent;
      }
    }

    internal void Remove()
    {
      if (Parent == null)
        throw new InvalidOperationException("This node does not have a parent");
      Parent.RemoveNode(this);
    }

    internal abstract void Visit(INodeVisitor visitor);

    public override string ToString()
    {
      using (var writer = new StringWriter())
      {
        var visitor = new HtmlVisitor(writer);
        Visit(visitor);
        visitor.Flush();
        return writer.ToString();
      }
    }
  }
}
