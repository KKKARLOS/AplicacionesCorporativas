
function init(){
}

function NodoSeleccionado() {
    //Obtengo el nodo seleccionado
    
    var treeView = eo_GetObject("Arbol");
    var selectedNode = treeView.getSelectedNode();
    if (selectedNode == null) return;
    if (selectedNode.getCheckState()==false) selectedNode.setCheckState(true)

    //alert(selectedNode.getText());
    //alert(selectedNode.getItemId());

}
function UpdateNodo() {
}
function addNodo() {
    var treeView = eo_GetObject("Arbol");
    var selectedNode = treeView.getSelectedNode();

    treeView.setSelectedNode(selectedNode, false)
}
function DeleteNodo() {
    var treeView = eo_GetObject("Arbol");
    var selectedNode = treeView.getSelectedNode();
    treeView.Nodes.Remove(selectedNode);
}

function GetNodos() {
    var treeView = eo_GetObject("Arbol");
    var selectedNode = treeView.getSelectedNode();

    for (var i = 0; i < selectedNode.ChildNodes.Count(); i++) {
        var node = selectedNode.ChildNodes.Item[i];
        alert(node.getText());
    }
}


function AddNode() {
    var tree = $find("<%= RadTreeView1.ClientID %>");
    tree.trackChanges();
    var node = new Telerik.Web.UI.RadTreeNode();
    node.set_text("New Node");
    tree.get_nodes().add(node);
    tree.commitChanges();
}

/*
function GetNodes() {
    var tree = $find("<%= RadTreeView1.ClientID %>");
    for (var i = 0; i < tree.get_nodes().get_count(); i++) {
        var node = tree.get_nodes().getNode(i);
        alert(node.get_text());
    }
}

function selectNode(text) {
    var treeView = $find("<%= RadTreeView2.ClientID %>");
    var node = treeView.findNodeByText(text);
    node.select();
}
function FindNode() {
    var tree = $find("<%= RadTreeView1.ClientID %>");
    var node = tree.findNodeByText("Child RadTreeNode 1");
    //... or ...
    var node1 = tree.findNodeByValue("3");
    //... or ...
    var node2 = tree.findNodeByAttribute("MyCustomAttribute", "Some Value");
    node.get_parent().expand();
    node.select();
}
*/




