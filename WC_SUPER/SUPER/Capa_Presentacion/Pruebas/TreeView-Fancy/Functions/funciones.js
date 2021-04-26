$.ui.fancytree.debugLevel = 1; // silence debug output

function logEvent(event, data, msg) {
    //        var args = $.isArray(args) ? args.join(", ") :
    msg = msg ? ": " + msg : "";
    $.ui.fancytree.info("Event('" + event.type + "', node=" + data.node + ")" + msg);
}
// --- Implement Cut/Copy/Paste --------------------------------------------          
var clipboardNode = null;
var pasteMode = null;
function copyPaste(action, node) {
    switch (action) {
        case "cut":
        case "copy":
            clipboardNode = node;
            pasteMode = action;
            break;
        case "paste":
            if (!clipboardNode) {
                alert("Clipoard is empty.");
                break;
            }
            if (pasteMode == "cut") {
                // Cut mode: check for recursion and remove source                                  
                var cb = clipboardNode.toDict(true);
                if (node.isDescendantOf(cb)) {
                    alert("Cannot move a node to it's sub node.");
                    return;
                }
                node.addChildren(cb);
                node.render();
                clipboardNode.remove();
            } else {
                // Copy mode: prevent duplicate keys:
                var cb = clipboardNode.toDict(true, function(dict) {
                    dict.title = "Copy of " + dict.title;
                    delete dict.key; // Remove key, so a new one will be created                                  
                });
                alert("cb = " + JSON.stringify(cb));
                //	node.addChildren(cb);  
                //  node.render();                                  
                node.applyPatch(cb);
            }
            clipboardNode = pasteMode = null;
            break;
        default:
            alert("Unhandled clipboard action '" + action + "'");
    }
};
// --- Contextmenu helper --------------------------------------------------

function bindContextMenu(span) {
    // Add context menu to this node:
    $(span).contextMenu({ menu: "myMenu" }, function(action, el, pos) {
        // The event was bound to the <span> tag, but the node object                          
        // is stored in the parent <li> tag                          
        var node = $.ui.fancytree.getNode(el);
        switch (action) {
            case "cut":
            case "copy":
            case "paste":
                copyPaste(action, node);
                break;
            default:
                alert("Todo: appply action '" + action + "' to node " + node);
        }
    });
};  

$(function() {

    $("#tree").fancytree({
          extensions: ["dnd", "edit", "menu"],
          menu: {
              selector: "#myMenu",
              position: { my: "center" },
              create: function(event, data) {
                  $.ui.fancytree.debug("Menu create ", data.$menu);
              },
              beforeOpen: function(event, data) {
                  //var node = data.node;
                  data.node.setActive(); 
                  //$.ui.fancytree.debug("Menu beforeOpen ", data.$menu, data.node);
              },
              open: function(event, data) {
                  $.ui.fancytree.debug("Menu open ", data.$menu, data.node);
              },
              focus: function(event, data) {
                  $.ui.fancytree.debug("Menu focus ", data.menuId, data.node);
              },
              select: function(event, data) {

                  var node = data.node;
                  alert("select " + data.cmd + " on " + node); 
              
                  //alert("Menu select " + data.menuId + ", " + data.node);
              },
              close: function(event, data) {
                  $.ui.fancytree.debug("Menu close ", data.$menu, data.node);
              }                            
          }, 

//        source: {url: "ajax-tree-plain.json"}, 
          edit: { 
              beforeClose: function(event, data){ 
                // Return false to prevent cancel/save (data.input is available) 
              }, 
              beforeEdit: function(event, data){ 
                // Return false to prevent edit mode 
              }, 
              close: function(event, data){ 
                // Editor was removed 
              }, 
              edit: function(event, data){ 
                // Editor was opened (available as data.input) 
              }, 
              save: function(event, data){ 
                // Save data.input.val() or return false to keep editor open 
                alert("save " + data.input.val()); 
              } 
          },
//          extensions: ["dnd", "edit"],
//        source: {
//            url: "ajax-tree-fs.json"
//        },
          dnd: {
            preventVoidMoves: true, // Prevent dropping nodes 'before self', etc. 
            preventRecursiveMoves: true, // Prevent dropping nodes on own descendants 
            autoExpandMS: 400,
            dragStart: function(node, data) {
                /** This function MUST be defined to enable dragging for the tree. 
                *  Return false to cancel dragging of node. 
                */
                return true;
            },
            dragEnter: function(node, data) {
                /** data.otherNode may be null for non-fancytree droppables. 
                *  Return false to disallow dropping on node. In this case 
                *  dragOver and dragLeave are not called. 
                *  Return 'over', 'before, or 'after' to force a hitMode. 
                *  Return ['before', 'after'] to restrict available hitModes. 
                *  Any other return value will calc the hitMode from the cursor position. 
                */
                // Prevent dropping a parent below another parent (only sort 
                // nodes under the same parent) 
                /*           if(node.parent !== data.otherNode.parent){ 
                return false; 
                } 
                // Don't allow dropping *over* a node (would create a child) 
                return ["before", "after"]; 
                */
                return true;
            },
            dragDrop: function(node, data) {
                /** This function MUST be defined to enable dropping of items on 
                *  the tree. 
                */
                data.otherNode.moveTo(node, data.hitMode);
            }
        },

        checkbox: true,
        
        // Tree events
        blurTree: function(event, data) {
            logEvent(event, data);
        },
        create: function(event, data) {
            logEvent(event, data);
        },
        init: function(event, data, flag) {
            logEvent(event, data, "flag=" + flag);
        },
        focusTree: function(event, data) {
            logEvent(event, data);
        },

        // Node events
        activate: function(event, data) {
            logEvent(event, data);
            var node = data.node;
            // acces node attributes
            $("#echoActive").text(node.title + ", key=" + node.key);
            if (!$.isEmptyObject(node.data)) {
                //					alert("custom node data: " + JSON.stringify(node.data));
            }
        },
        beforeActivate: function(event, data) {
            logEvent(event, data, "current state=" + data.node.isActive());
            // return false to prevent default behavior (i.e. activation)
            //              return false;
        },
        beforeExpand: function(event, data) {
            logEvent(event, data, "current state=" + data.node.isExpanded());
            // return false to prevent default behavior (i.e. expanding or collapsing)
            //				return false;
        },
        beforeSelect: function(event, data) {
            //				console.log("select", event.originalEvent);
            logEvent(event, data, "current state=" + data.node.isSelected());
            // return false to prevent default behavior (i.e. selecting or deselecting)
            				if( data.node.isFolder() ){
            					return false;
            				}
        },
        blur: function(event, data) {
            logEvent(event, data);
            $("#echoFocused").text("-");
        },
        click: function(event, data) {
            // Close menu on click                                  
            if( $(".contextMenu:visible").length > 0 ){
                $(".contextMenu").hide();
                //                                      return false; 
            }  
        },
        mouseon: function(event, data) {
            logEvent(event, data, ", targetType=" + data.targetType);
            // return false to prevent default behavior (i.e. activation, ...)
            //return false;
        },
        collapse: function(event, data) {
            logEvent(event, data);
        },
        dblclick: function(event, data) {
            logEvent(event, data);
            //				data.node.toggleSelect();
        },
        deactivate: function(event, data) {
            logEvent(event, data);
            $("#echoActive").text("-");
        },
        expand: function(event, data) {
            logEvent(event, data);
        },
        focus: function(event, data) {
            logEvent(event, data);
            $("#echoFocused").text(data.node.title);
        },
        keydown: function(event, data) {
            var node = data.node;
            // Eat keyboard events, when a menu is open                                  
            if ($(".contextMenu:visible").length > 0)
                return false;
            switch (event.which) {
                // Open context menu on [Space] key (simulate right click) 
                case 32: // [Space]
                    $(node.span).trigger("mousedown", {
                        preventDefault: true,
                        button: 2
                    })
								.trigger("mouseup", {
								    preventDefault: true,
								    pageX: node.span.offsetLeft,
								    pageY: node.span.offsetTop,
								    button: 2
								});
                    return false;

                    // Handle Ctrl-C, -X and -V
                case 67:
                    if (event.ctrlKey) { // Ctrl-C
                        copyPaste("copy", node);
                        return false;
                    }
                    break;
                case 86:
                    if (event.ctrlKey) { // Ctrl-V
                        copyPaste("paste", node);
                        return false;
                    }
                    break;
                case 88:
                    if (event.ctrlKey) { // Ctrl-X
                        copyPaste("cut", node);
                        return false;
                    }
                    break;
            }
        }, 
        keypress: function(event, data) {
            // currently unused
            logEvent(event, data);
        },
        /*Bind context menu for every node when its DOM element is created. 
        We do it here, so we can also bind to lazy nodes, which do not 
        exist at load-time. (abeautifulsite.net menu control does not 
        support event delegation)*/
        //createNode: function(event, data) {
        //    bindContextMenu(data.node.span);
        //},         
        lazyLoad: function(event, data) {
            logEvent(event, data);
            // return children or any other node source
            data.result = { url: "ajax-sub2.json" };
            //				data.result = [
            //					{title: "A Lazy node", lazy: true},
            //					{title: "Another node", selected: true}
            //					];
        },
        loadChildren: function(event, data) {
            logEvent(event, data);
        },
        postProcess: function(event, data) {
            logEvent(event, data);
//            // either modify the ajax response directly
//            data.response[0].title += " - hello from postProcess";
//            // or setup and return a new response object
//            //				data.result = [{title: "set by postProcess"}];
        },
        removeNode: function(event, data) {
            // Optionally release resources
            logEvent(event, data);
        },
        renderNode: function(event, data) {
            // Optionally tweak data.node.span
            //              $(data.node.span).text(">>" + data.node.title);
            logEvent(event, data);
        },
        renderTitle: function(event, data) {
            // NOTE: may be removed!
            // When defined, must return a HTML string for the node title
            logEvent(event, data);
            //				return "new title";
        },
        select: function(event, data) {
            logEvent(event, data, "current state=" + data.node.isSelected());
            var s = data.tree.getSelectedNodes().join(", ");
            $("#echoSelected").text(s);
        },
        save: function(event, data){
          // data.node.title still contains the original value.
          // Return false to keep editor open, or
          // save data.input.val() to your backend and return true (node.title will 
          // be updated then).
          alert("save " + data.input.val());
        }
             
    }).bind("fancytreeactivate", function(event, data) {
        // alternative way to bind to 'activate' event
        //		    logEvent(event, data);
    });

    $("#btnCreateNode").click(function(event) {
        var node = $("#tree").fancytree("getActiveNode");
        if (node == null) return;
        newData = { title: "New Node" },
		newSibling = node.getParent().addChildren(newData, node.getNextSibling());
    });

    $("#btnCreateNode2").click(function(event) {
        var rootNode = $("#tree").fancytree("getRootNode");
        var childNode = rootNode.addChildren({
            title: "Programatically addded nodes",
            tooltip: "This folder and all child nodes were added programmatically.",
            folder: true
        });
        childNode.addChildren({
            title: "Document using a custom icon",
            icon: "customdoc1.gif"
        });
    });

    $("#btnSelect").click(function(event) {
        var node = $("#tree").fancytree("getActiveNode");
        if (node == null) return;
        node.setSelected(!node.isSelected());
        alert(node.key);
        alert(node.title);
    });

    $("#btnRemove").click(function(event) {
        var node = $("#tree").fancytree("getActiveNode");
        if (node == null) return;
        node.remove();
    });
});


function init() {
}

/*
$("#tree").contextmenu({
delegate: "span.fancytree-title",
//      menu: "#options", 
menu: [
{ title: "Cut", cmd: "cut", uiIcon: "ui-icon-scissors" },
{ title: "Copy", cmd: "copy", uiIcon: "ui-icon-copy" },
{ title: "Paste", cmd: "paste", uiIcon: "ui-icon-clipboard", disabled: false },
{ title: "----" },
{ title: "Edit", cmd: "edit", uiIcon: "ui-icon-pencil", disabled: true },
{ title: "Delete", cmd: "delete", uiIcon: "ui-icon-trash", disabled: true },
{ title: "More", children: [
{ title: "Sub 1", cmd: "sub1" },
{ title: "Sub 2", cmd: "sub1" }
]
}
],
beforeOpen: function(event, ui) {
var node = $.ui.fancytree.getNode(ui.target);
//                node.setFocus(); 
node.setActive();
},
select: function(event, ui) {
var node = $.ui.fancytree.getNode(ui.target);
alert("select " + ui.cmd + " on " + node);
}
}); 
*/