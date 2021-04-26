/*
    Author: Romulo do Nascimento Ferreira
    E-mail: romulo.nf@mgmail.com
 
    Drag & Drop Table Columns
*/
 
 
/* parameters
 
    id: id of the table that will have drag & drop function
 
*/
 
function dragTable(id) {
    // store the cell that will be dragged
    this.draggedCell = null
    // true if ghostTd exists
    this.ghostCreated = false
    // store the table itselfs
    this.table = document.getElementById(id)
    // store every row of the table
    this.tableRows = this.table.getElementsByTagName("tr")
    // create a handler array, usualy the ths in the thead, if not possible the first row of tds
    this.handler = this.table.getElementsByTagName("th").length > 0 ? this.table.getElementsByTagName("th") : this.table.tBodies[0].rows[0].getElementsByTagName("td")
    // create a cell array
    this.cells = this.table.getElementsByTagName("td")
    // store the max index of the column when dropped
    this.maxIndex = this.handler.length
    // store the horizontal mouse position
    this.x;
    // store the vertical mouse position
    this.y;
    // store the index of the column that will be dragged
    this.oldIndex;
    // store the index of the destionation of the column
    this.newIndex;

    for (x = 0; x < this.handler.length; x++) {
        //Modificación: solo tratamos las columnas que son "Dimensiones"
        if (!this.handler[x].hasAttribute("dimension")) continue;

//        //this.handler[x].style.cursor = strCurMM;
//        // associate the object with the cells
//        this.handler[x].dragObj = this
//        // override the default action when mousedown and dragging
//        this.handler[x].onselectstart = function() { return false }
//        // fire the drag action and return false to prevent default action of selecting the text
//        this.handler[x].onmousedown = function(e) { this.dragObj.drag(this,e); return false }
//        // visual effect
//        this.handler[x].onmouseover = function(e) { this.dragObj.dragEffect(this,e) }
//        this.handler[x].onmouseout = function(e) { this.dragObj.dragEffect(this,e) }
//        this.handler[x].onmouseup = function(e) { this.dragObj.dragEffect(this,e) }

        // associate the object with the cells
        this.handler[x].children[0].dragObj = this
        // override the default action when mousedown and dragging
        this.handler[x].children[0].onselectstart = function(e) { return false } 
        this.handler[x].children[0].ondragstart = function(e) { return false }
        // fire the drag action and return false to prevent default action of selecting the text
        this.handler[x].children[0].onmousedown = function(e) { this.dragObj.drag(this, e); return false }
        // visual effect
        this.handler[x].children[0].onmouseover = function(e) { this.dragObj.dragEffect(this, e) }
        this.handler[x].children[0].onmouseout = function(e) { this.dragObj.dragEffect(this, e) }
        this.handler[x].children[0].onmouseup = function(e) { this.dragObj.dragEffect(this, e) }
    }

    for (x = 0; x < this.cells.length; x++) {
        //Modificación: solo tratamos las celdas correspondientes a columnas que son "Dimensiones"
        if (!this.tableRows[0].cells[this.cells[x].cellIndex].hasAttribute("dimension")) continue;
        
        this.cells[x].dragObj = this
        // visual effect
        this.cells[x].onmouseover = function(e) { this.dragObj.dragEffect(this,e) }
        this.cells[x].onmouseout = function(e) { this.dragObj.dragEffect(this,e) }
        this.cells[x].onmouseup = function(e) { this.dragObj.dragEffect(this,e) }
    }
}

dragTable.prototype.dragEffect = function(cell, e) {
    // assign event to variable e
    if (!e) e = window.event

    while (cell.tagName.toUpperCase() != "TD" && cell.tagName.toUpperCase() != "TH") {
        cell = cell.parentNode;
    }
    // return if the cell being hovered is the same as the one being dragged
    if (cell.cellIndex == this.oldIndex) return

    else {
        // if there is a cell being dragged
        if (this.draggedCell) {
            // change class to give a visual effect
            e.type == "mouseover" ? this.handler[cell.cellIndex].className = "hovering" : this.handler[cell.cellIndex].className = ""
        }
    }
}

dragTable.prototype.drag = function(cell, e) {
    while (cell.tagName.toUpperCase() != "TD" && cell.tagName.toUpperCase() != "TH") {
        cell = cell.parentNode;
    }
    // reference of the cell that is being dragged
    this.draggedCell = cell

    // change class for visual effect
    this.draggedCell.className = "dragging"

    // store the index of the cell that is being dragged
    this.oldIndex = (cell.tagName == "TD" || cell.tagName == "TH") ? cell.cellIndex : cell.parentNode.cellIndex;

    // create the ghost td
    this.createGhostTd(e)
    // start the engine
    this.dragEngine(true)
}
 
dragTable.prototype.createGhostTd = function(e) {
    // if ghost exists return
    if (this.ghostCreated) return
    // assign event to variable e
    if (!e) e = window.event
    // horizontal position
    this.x = e.pageX ? e.pageX : e.clientX + document.documentElement.scrollLeft
    // vertical position
    this.y = e.pageY ? e.pageY : e.clientY + document.documentElement.scrollTop
     
        // create the ghost td (visual effect)
        this.ghostTd = document.createElement("div")
        this.ghostTd.className = "ghostTd"
        this.ghostTd.style.top = this.y + 5 + "px"
        this.ghostTd.style.left = this.x + 10 + "px"
        // ghost td receives the content of the dragged cell
        this.ghostTd.innerHTML = this.handler[this.oldIndex].innerHTML
        document.getElementsByTagName("body")[0].appendChild(this.ghostTd)
     
    // assign a flag to see if ghost is created
    this.ghostCreated = true
}

dragTable.prototype.drop = function(dragObj, e) {
    // assign event to variable e
    if (!e) e = window.event
    // store the target of the event - mouseup
    e.targElm = e.target ? e.target : e.srcElement

    // end the engine
    dragObj.dragEngine(false, dragObj)

    // remove the ghostTd
    dragObj.ghostTd.parentNode.removeChild(dragObj.ghostTd)

    // remove ghost created flag
    this.ghostCreated = false

    //Modificación:
    while (e.targElm.tagName.toLowerCase() != "td" && e.targElm.tagName.toLowerCase() != "th") {
        if (e.targElm.tagName.toLowerCase() == "html") break
        e.targElm = e.targElm.parentNode
    }

    // store the index of the target, if it have one
    if (typeof (e.targElm.cellIndex) != "undefined") {
        //Modificación:
        if (!this.handler[e.targElm.cellIndex].hasAttribute("dimension")) {
            for (x = 0; x < this.handler.length; x++) {
                if (this.handler[x].hasAttribute("dimension") && this.handler[x].className != "") {
                    this.handler[x].className = "";
                    break;
                }
            }
            return;
        }

        checkTable = e.targElm

        // ascend in the dom beggining in the targeted element and ending in a table or the body tag
        while (checkTable.tagName.toLowerCase() != "table") {
            if (checkTable.tagName.toLowerCase() == "html") break
            checkTable = checkTable.parentNode
        }

        // check if the table where the column was dropped is equal to the object table
        checkTable == this.table ? this.newIndex = e.targElm.cellIndex : false
    }

    var sDimOrigen = this.handler[this.oldIndex].getAttribute("dimension");
    var sDimDestino = this.handler[(typeof (this.newIndex) != "undefined") ? this.newIndex : 0].getAttribute("dimension");

    // start the function to sort the column
    //var res = dragObj.sortColumn(this.oldIndex, this.newIndex)
    /* No reordeno las columnas porque lo hace la consulta de bd. */
    var res = true;
    if (this.oldIndex == null || this.oldIndex == this.newIndex) {
        res = false;
    }

    // remove visual effect from column being dragged
    this.draggedCell.className = ""
    // clear the variable
    this.draggedCell = null

    if (res) {
        $I("tblDimensiones").moveRow($I("trdim_" + sDimOrigen).rowIndex, $I("trdim_" + sDimDestino).rowIndex);
        setIndicadoresAux();
    }
}

dragTable.prototype.sortColumn = function(o, d) {
    // returns if destionation dont have a valid index
    if (d == null) return
    // returns if origin is equals to the destination
    if (o == d) return

    // loop through every row
    for (x = 0; x < this.tableRows.length; x++) {
        // array with the cells of the row x
        tds = this.tableRows[x].cells
        // remove this cell from the row
        var cell = this.tableRows[x].removeChild(tds[o])
        // insert the cell in the new index
        if (d + 1 >= this.maxIndex) {
            this.tableRows[x].appendChild(cell)
        }
        else {
            this.tableRows[x].insertBefore(cell, tds[d])
        }
    }
    return true;
}
 
dragTable.prototype.dragEngine = function(boolean,dragObj) {
    var _this = this
    // fire the drop function
    document.documentElement.onmouseup = boolean ? function(e) { _this.drop(_this,e) } : null
    // capture the mouse coords
    document.documentElement.onmousemove = boolean ? function(e) { _this.getCoords(_this,e) } : null
}
 
dragTable.prototype.getCoords = function(dragObj,e) {
    if (!e) e = window.event
     
    // horizontal position
    dragObj.x = e.pageX ? e.pageX : e.clientX + document.documentElement.scrollLeft
    // vertical position
    dragObj.y = e.pageY ? e.pageY : e.clientY + document.documentElement.scrollTop
 
    if (dragObj.ghostTd) {
        // make the ghostTd follow the mouse
        dragObj.ghostTd.style.top = dragObj.y + 5 + "px"
        dragObj.ghostTd.style.left = dragObj.x + 10 + "px"
    }
}
 