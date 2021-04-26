function init(){

}


function LeerFichero(filespec)
{
   var fso, f, s, ForReading, i;
   ForReading = 1, s = "";
   fso = new ActiveXObject("Scripting.FileSystemObject");
   f = fso.OpenTextFile(filespec, ForReading, false);
   i = 1;
   while (!f.AtEndOfStream){
      //s += f.ReadLine();
      if (i<10) alert("Línea "+i+": "+ f.ReadLine());
      i++;
   }
   //alert(i);
   f.Close();
   //return(s);
}

