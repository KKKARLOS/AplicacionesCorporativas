	var nWidth = 76;
	var nHeight= 73;
	function mostrarDIV(){
		if (nWidth < 700) nWidth = nWidth + 8;
		if (nHeight < 500) nHeight = nHeight + 6;
		$I("divGlobal").style.clip = "rect(auto "+ nWidth +" "+ nHeight +" auto)";
		$I("divImg").style.left = nWidth - 74;
		$I("divImg").style.top = nHeight - 76;
		$I("imgSol").src = "../../../Images/imgSolapa4.gif";
	    $I("divGlobal").style.backgroundColor = "white";
		if (nWidth < 700 || nHeight < 500) setTimeout("mostrarDIV()", 1);
		else{
		    $I("divGlobal").style.border = "1px navy solid";
		    $I("divImg").style.top = nHeight - 82;
		}
	}
	function ocultarDIV(){
		if (nWidth > 76) nWidth = nWidth - 8;
		if (nHeight > 73) nHeight = nHeight - 6;
		$I("divGlobal").style.clip = "rect(auto "+ nWidth +" "+ nHeight +" auto)";
		$I("divImg").style.left = nWidth - 76;
		$I("divImg").style.top = nHeight - 73;
		if (nWidth > 76 || nHeight > 73) setTimeout("ocultarDIV()", 1);
		else{
		    $I("divGlobal").style.backgroundColor = "#D8E5EB";
			$I("imgSol").src = "../../../Images/imgSolapa2.gif";
		    $I("divGlobal").style.clip = "rect(auto 73 76 auto)";
		    $I("divGlobal").style.border = "";
		}
	}
	function bCapaVisible(){
		if ($I("divGlobal").style.clip == "rect(auto 700px 505px auto)") return true;
		else return false;
	}
	function mostrar(){
	    //alert($I("divGlobal").style.clip);
		if (bCapaVisible()) ocultarDIV();
		else mostrarDIV();
}