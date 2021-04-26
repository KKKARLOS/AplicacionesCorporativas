<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <xsl:output method="xml" indent="yes"/>

  <!-- Start root -->
  <xsl:template match="/"  xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main">
    <xsl:apply-templates select="CustomerReportTemplateDTO"/>
  </xsl:template>
  <!-- End root -->

  <!-- Start CustomerReportTemplate -->
  <xsl:template match="CustomerReportTemplateDTO" xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main">
    <wordDoc>
      <!-- Start header -->
      <w:hdr xmlns:ve="http://schemas.openxmlformats.org/markup-compatibility/2006" xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships" xmlns:m="http://schemas.openxmlformats.org/officeDocument/2006/math" xmlns:v="urn:schemas-microsoft-com:vml" xmlns:wp="http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing" xmlns:w10="urn:schemas-microsoft-com:office:word" xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main" xmlns:wne="http://schemas.microsoft.com/office/word/2006/wordml">
        <w:tbl>
          <w:tblPr>
            <w:tblStyle w:val="Tablaconcuadrcula" />
            <w:tblW w:w="8796" w:type="dxa" />
            <w:tblLayout w:type="fixed" />
            <w:tblCellMar>
              <w:left w:w="0" w:type="dxa" />
              <w:right w:w="0" w:type="dxa" />
            </w:tblCellMar>
            <w:tblLook w:val="04A0" />
          </w:tblPr>
          <w:tblGrid>
            <w:gridCol w:w="1276" />
            <w:gridCol w:w="4142" />
            <w:gridCol w:w="92" />
            <w:gridCol w:w="1043" />
            <w:gridCol w:w="2243" />
          </w:tblGrid>
          <w:tr w:rsidRPr="00507449" w:rsidR="004127E4" w:rsidTr="00010551">
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="1276" w:type="dxa" />
                <w:vMerge w:val="restart" />
                <w:tcBorders>
                  <w:top w:val="nil" />
                  <w:left w:val="nil" />
                  <w:bottom w:val="nil" />
                </w:tcBorders>
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00507449" w:rsidR="00D52016" w:rsidP="00D52016" w:rsidRDefault="00507449">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:jc w:val="center" />
                  <w:rPr>
                    <w:rFonts w:cs="Calibri" w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                </w:pPr>
                <w:r w:rsidRPr="00507449">
                  <w:rPr>
                    <w:rFonts w:cs="Calibri" w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                  <w:t>
                    <xsl:text>&lt;html&gt;&lt;body&gt;&lt;img src="CompanyLogo.png" width="60" height = "60" /&gt;&lt;/body&gt;&lt;/html&gt;</xsl:text>
                  </w:t>
                </w:r>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="4142" w:type="dxa" />
                <w:vMerge w:val="restart" />
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00507449" w:rsidR="00D52016" w:rsidP="00D52016" w:rsidRDefault="00507449">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:jc w:val="center" />
                  <w:rPr>
                    <w:rFonts w:cs="Calibri" w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:b />
                    <w:sz w:val="32" />
                    <w:szCs w:val="32" />
                  </w:rPr>
                </w:pPr>
                <w:r w:rsidRPr="00507449">
                  <w:rPr>
                    <w:rFonts w:cs="Calibri" w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:b />
                    <w:sz w:val="32" />
                    <w:szCs w:val="32" />
                  </w:rPr>
                  <w:t>
                    <xsl:value-of select="ReportTitle"/>
                  </w:t>
                </w:r>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="92" w:type="dxa" />
                <w:tcBorders>
                  <w:top w:val="nil" />
                  <w:bottom w:val="nil" />
                </w:tcBorders>
              </w:tcPr>
              <w:p w:rsidRPr="00507449" w:rsidR="00D52016" w:rsidP="00D52016" w:rsidRDefault="00D52016">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                  </w:rPr>
                </w:pPr>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="1043" w:type="dxa" />
                <w:tcBorders>
                  <w:bottom w:val="nil" />
                  <w:right w:val="nil" />
                </w:tcBorders>
                <w:tcMar>
                  <w:left w:w="0" w:type="dxa" />
                  <w:right w:w="113" w:type="dxa" />
                </w:tcMar>
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00507449" w:rsidR="00D52016" w:rsidP="00507449" w:rsidRDefault="00D52016">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:jc w:val="right" />
                  <w:rPr>
                    <w:rFonts w:cs="Calibri" w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="16" />
                    <w:szCs w:val="16" />
                  </w:rPr>
                </w:pPr>
                <w:r w:rsidRPr="00507449">
                  <w:rPr>
                    <w:rFonts w:cs="Calibri" w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="16" />
                    <w:szCs w:val="16" />
                  </w:rPr>
                  <w:t>Nº Episodio</w:t>
                </w:r>
                <w:r w:rsidR="004127E4">
                  <w:rPr>
                    <w:rFonts w:cs="Calibri" w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="16" />
                    <w:szCs w:val="16" />
                  </w:rPr>
                  <w:t>:</w:t>
                </w:r>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="2243" w:type="dxa" />
                <w:tcBorders>
                  <w:left w:val="nil" />
                  <w:bottom w:val="nil" />
                </w:tcBorders>
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00507449" w:rsidR="00D52016" w:rsidP="00D52016" w:rsidRDefault="00D52016">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:rPr>
                    <w:rFonts w:cs="Calibri" w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                </w:pPr>
                <w:r w:rsidRPr="00507449">
                  <w:rPr>
                    <w:rFonts w:cs="Calibri" w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                  <w:t>
                    <xsl:value-of select="MedEpisodeNumber" />
                  </w:t>
                </w:r>
              </w:p>
            </w:tc>
          </w:tr>
          <w:tr w:rsidRPr="00507449" w:rsidR="004127E4" w:rsidTr="00010551">
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="1276" w:type="dxa" />
                <w:vMerge />
                <w:tcBorders>
                  <w:top w:val="nil" />
                  <w:left w:val="nil" />
                  <w:bottom w:val="nil" />
                </w:tcBorders>
              </w:tcPr>
              <w:p w:rsidRPr="00507449" w:rsidR="00D52016" w:rsidP="00D52016" w:rsidRDefault="00D52016">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                  </w:rPr>
                </w:pPr>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="4142" w:type="dxa" />
                <w:vMerge />
              </w:tcPr>
              <w:p w:rsidRPr="00507449" w:rsidR="00D52016" w:rsidP="00D52016" w:rsidRDefault="00D52016">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                  </w:rPr>
                </w:pPr>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="92" w:type="dxa" />
                <w:tcBorders>
                  <w:top w:val="nil" />
                  <w:bottom w:val="nil" />
                </w:tcBorders>
              </w:tcPr>
              <w:p w:rsidRPr="00507449" w:rsidR="00D52016" w:rsidP="00D52016" w:rsidRDefault="00D52016">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                  </w:rPr>
                </w:pPr>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="1043" w:type="dxa" />
                <w:tcBorders>
                  <w:top w:val="nil" />
                  <w:bottom w:val="nil" />
                  <w:right w:val="nil" />
                </w:tcBorders>
                <w:tcMar>
                  <w:left w:w="0" w:type="dxa" />
                  <w:right w:w="113" w:type="dxa" />
                </w:tcMar>
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00507449" w:rsidR="00D52016" w:rsidP="00507449" w:rsidRDefault="00D52016">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:jc w:val="right" />
                  <w:rPr>
                    <w:rFonts w:cs="Calibri" w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="16" />
                    <w:szCs w:val="16" />
                  </w:rPr>
                </w:pPr>
                <w:r w:rsidRPr="00507449">
                  <w:rPr>
                    <w:rFonts w:cs="Calibri" w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="16" />
                    <w:szCs w:val="16" />
                  </w:rPr>
                  <w:t>Nº Historia</w:t>
                </w:r>
                <w:r w:rsidR="004127E4">
                  <w:rPr>
                    <w:rFonts w:cs="Calibri" w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="16" />
                    <w:szCs w:val="16" />
                  </w:rPr>
                  <w:t>:</w:t>
                </w:r>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="2243" w:type="dxa" />
                <w:tcBorders>
                  <w:top w:val="nil" />
                  <w:left w:val="nil" />
                  <w:bottom w:val="nil" />
                </w:tcBorders>
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00507449" w:rsidR="00D52016" w:rsidP="00D52016" w:rsidRDefault="00507449">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:rPr>
                    <w:rFonts w:cs="Calibri" w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                </w:pPr>
                <w:r w:rsidRPr="00507449">
                  <w:rPr>
                    <w:rFonts w:cs="Calibri" w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                  <w:t>
                    <xsl:value-of select="CustomerCH" />
                  </w:t>
                </w:r>
              </w:p>
            </w:tc>
          </w:tr>
          <w:tr w:rsidRPr="00507449" w:rsidR="004127E4" w:rsidTr="00010551">
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="1276" w:type="dxa" />
                <w:vMerge />
                <w:tcBorders>
                  <w:top w:val="nil" />
                  <w:left w:val="nil" />
                  <w:bottom w:val="nil" />
                </w:tcBorders>
              </w:tcPr>
              <w:p w:rsidRPr="00507449" w:rsidR="00D52016" w:rsidP="00D52016" w:rsidRDefault="00D52016">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                  </w:rPr>
                </w:pPr>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="4142" w:type="dxa" />
                <w:vMerge />
              </w:tcPr>
              <w:p w:rsidRPr="00507449" w:rsidR="00D52016" w:rsidP="00D52016" w:rsidRDefault="00D52016">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                  </w:rPr>
                </w:pPr>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="92" w:type="dxa" />
                <w:tcBorders>
                  <w:top w:val="nil" />
                  <w:bottom w:val="nil" />
                </w:tcBorders>
              </w:tcPr>
              <w:p w:rsidRPr="00507449" w:rsidR="00D52016" w:rsidP="00D52016" w:rsidRDefault="00D52016">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                  </w:rPr>
                </w:pPr>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="1043" w:type="dxa" />
                <w:tcBorders>
                  <w:top w:val="nil" />
                  <w:bottom w:val="nil" />
                  <w:right w:val="nil" />
                </w:tcBorders>
                <w:tcMar>
                  <w:left w:w="0" w:type="dxa" />
                  <w:right w:w="113" w:type="dxa" />
                </w:tcMar>
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00507449" w:rsidR="00D52016" w:rsidP="00507449" w:rsidRDefault="00D52016">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:jc w:val="right" />
                  <w:rPr>
                    <w:rFonts w:cs="Calibri" w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="16" />
                    <w:szCs w:val="16" />
                  </w:rPr>
                </w:pPr>
                <w:r w:rsidRPr="00507449">
                  <w:rPr>
                    <w:rFonts w:cs="Calibri" w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="16" />
                    <w:szCs w:val="16" />
                  </w:rPr>
                  <w:t>Nombre</w:t>
                </w:r>
                <w:r w:rsidR="004127E4">
                  <w:rPr>
                    <w:rFonts w:cs="Calibri" w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="16" />
                    <w:szCs w:val="16" />
                  </w:rPr>
                  <w:t>:</w:t>
                </w:r>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="2243" w:type="dxa" />
                <w:tcBorders>
                  <w:top w:val="nil" />
                  <w:left w:val="nil" />
                  <w:bottom w:val="nil" />
                </w:tcBorders>
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00507449" w:rsidR="00D52016" w:rsidP="00D52016" w:rsidRDefault="00507449">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:rPr>
                    <w:rFonts w:cs="Calibri" w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                </w:pPr>
                <w:r w:rsidRPr="00507449">
                  <w:rPr>
                    <w:rFonts w:cs="Calibri" w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                  <w:t>
                    <xsl:value-of select="FullName" />
                  </w:t>
                </w:r>
              </w:p>
            </w:tc>
          </w:tr>
          <w:tr w:rsidRPr="00507449" w:rsidR="004127E4" w:rsidTr="00010551">
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="1276" w:type="dxa" />
                <w:vMerge />
                <w:tcBorders>
                  <w:top w:val="nil" />
                  <w:left w:val="nil" />
                  <w:bottom w:val="nil" />
                </w:tcBorders>
              </w:tcPr>
              <w:p w:rsidRPr="00507449" w:rsidR="00D52016" w:rsidP="00D52016" w:rsidRDefault="00D52016">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                  </w:rPr>
                </w:pPr>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="4142" w:type="dxa" />
                <w:vMerge />
              </w:tcPr>
              <w:p w:rsidRPr="00507449" w:rsidR="00D52016" w:rsidP="00D52016" w:rsidRDefault="00D52016">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                  </w:rPr>
                </w:pPr>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="92" w:type="dxa" />
                <w:tcBorders>
                  <w:top w:val="nil" />
                  <w:bottom w:val="nil" />
                </w:tcBorders>
              </w:tcPr>
              <w:p w:rsidRPr="00507449" w:rsidR="00D52016" w:rsidP="00D52016" w:rsidRDefault="00D52016">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                  </w:rPr>
                </w:pPr>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="1043" w:type="dxa" />
                <w:tcBorders>
                  <w:top w:val="nil" />
                  <w:right w:val="nil" />
                </w:tcBorders>
                <w:tcMar>
                  <w:left w:w="0" w:type="dxa" />
                  <w:right w:w="113" w:type="dxa" />
                </w:tcMar>
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00507449" w:rsidR="00D52016" w:rsidP="00507449" w:rsidRDefault="00D52016">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:jc w:val="right" />
                  <w:rPr>
                    <w:rFonts w:cs="Calibri" w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="16" />
                    <w:szCs w:val="16" />
                  </w:rPr>
                </w:pPr>
                <w:r w:rsidRPr="00507449">
                  <w:rPr>
                    <w:rFonts w:cs="Calibri" w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="16" />
                    <w:szCs w:val="16" />
                  </w:rPr>
                  <w:t>Fecha</w:t>
                </w:r>
                <w:r w:rsidR="00010551">
                  <w:rPr>
                    <w:rFonts w:cs="Calibri" w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="16" />
                    <w:szCs w:val="16" />
                  </w:rPr>
                  <w:t>:</w:t>
                </w:r>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="2243" w:type="dxa" />
                <w:tcBorders>
                  <w:top w:val="nil" />
                  <w:left w:val="nil" />
                </w:tcBorders>
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00507449" w:rsidR="00D52016" w:rsidP="00D52016" w:rsidRDefault="00507449">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:rPr>
                    <w:rFonts w:cs="Calibri" w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                </w:pPr>
                <w:r w:rsidRPr="00507449">
                  <w:rPr>
                    <w:rFonts w:cs="Calibri" w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                  <w:t>
                    <xsl:value-of select="MedEpStartDateTime" />
                  </w:t>
                </w:r>
              </w:p>
            </w:tc>
          </w:tr>
        </w:tbl>
        <w:p w:rsidRPr="00D52016" w:rsidR="00D52016" w:rsidP="00507449" w:rsidRDefault="00D52016">
          <w:pPr>
            <w:pStyle w:val="Encabezado" />
            <w:jc w:val="center" />
          </w:pPr>
        </w:p>
      </w:hdr>
      <!-- End header -->

      <!-- Start body -->
      <w:document xmlns:ve="http://schemas.openxmlformats.org/markup-compatibility/2006" xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships" xmlns:m="http://schemas.openxmlformats.org/officeDocument/2006/math" xmlns:v="urn:schemas-microsoft-com:vml" xmlns:wp="http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing" xmlns:w10="urn:schemas-microsoft-com:office:word" xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main" xmlns:wne="http://schemas.microsoft.com/office/word/2006/wordml">
        <w:body>
          <xsl:apply-templates select="ReportTemplates"/>

          <w:sectPr w:rsidR="009529E9" w:rsidSect="009529E9">
            <w:headerReference w:type="even" r:id="rId7" />
            <w:headerReference w:type="default" r:id="rId8" />
            <w:footerReference w:type="even" r:id="rId9" />
            <w:footerReference w:type="default" r:id="rId10" />
            <w:headerReference w:type="first" r:id="rId11" />
            <w:footerReference w:type="first" r:id="rId12" />
            <w:pgSz w:w="11906" w:h="16838" />
            <w:pgMar w:top="1417" w:right="1701" w:bottom="1417" w:left="1701" w:header="708" w:footer="708" w:gutter="0" />
            <w:cols w:space="708" />
            <w:docGrid w:linePitch="360" />
          </w:sectPr>
        </w:body>
      </w:document>
      <!-- End body-->

      <!-- Start footer -->
      <w:ftr xmlns:ve="http://schemas.openxmlformats.org/markup-compatibility/2006" xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships" xmlns:m="http://schemas.openxmlformats.org/officeDocument/2006/math" xmlns:v="urn:schemas-microsoft-com:vml" xmlns:wp="http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing" xmlns:w10="urn:schemas-microsoft-com:office:word" xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main" xmlns:wne="http://schemas.microsoft.com/office/word/2006/wordml">
        <w:tbl>
          <w:tblPr>
            <w:tblStyle w:val="Tablaconcuadrcula" />
            <w:tblW w:w="8788" w:type="dxa" />
            <w:tblLayout w:type="fixed" />
            <w:tblCellMar>
              <w:left w:w="0" w:type="dxa" />
              <w:right w:w="0" w:type="dxa" />
            </w:tblCellMar>
            <w:tblLook w:val="04A0" />
          </w:tblPr>
          <w:tblGrid>
            <w:gridCol w:w="1732" />
            <w:gridCol w:w="92" />
            <w:gridCol w:w="3313" />
            <w:gridCol w:w="91" />
            <w:gridCol w:w="1224" />
            <w:gridCol w:w="2336" />
          </w:tblGrid>
          <w:tr w:rsidRPr="00507449" w:rsidR="00010551" w:rsidTr="00010551">
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="1722" w:type="dxa" />
                <w:vMerge w:val="restart" />
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="004127E4" w:rsidR="00010551" w:rsidP="004127E4" w:rsidRDefault="00010551">
                <w:pPr>
                  <w:pStyle w:val="Piedepgina" />
                  <w:jc w:val="center" />
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="16" />
                    <w:szCs w:val="16" />
                  </w:rPr>
                </w:pPr>
                <w:r w:rsidRPr="004127E4">
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="16" />
                    <w:szCs w:val="16" />
                  </w:rPr>
                  <w:t>23/05/2011</w:t>
                </w:r>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="91" w:type="dxa" />
                <w:vMerge w:val="restart" />
                <w:tcBorders>
                  <w:top w:val="nil" />
                  <w:bottom w:val="nil" />
                </w:tcBorders>
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="004127E4" w:rsidR="00010551" w:rsidP="004127E4" w:rsidRDefault="00010551">
                <w:pPr>
                  <w:pStyle w:val="Piedepgina" />
                  <w:jc w:val="center" />
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="16" />
                    <w:szCs w:val="16" />
                  </w:rPr>
                </w:pPr>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="3293" w:type="dxa" />
                <w:vMerge w:val="restart" />
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="004127E4" w:rsidR="00010551" w:rsidP="004127E4" w:rsidRDefault="00010551">
                <w:pPr>
                  <w:pStyle w:val="Piedepgina" />
                  <w:jc w:val="center" />
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="16" />
                    <w:szCs w:val="16" />
                  </w:rPr>
                </w:pPr>
                <w:r w:rsidRPr="004127E4">
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="16" />
                    <w:szCs w:val="16" />
                  </w:rPr>
                  <w:t xml:space="preserve">Página </w:t>
                </w:r>
                <w:r w:rsidRPr="004127E4">
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="16" />
                    <w:szCs w:val="16" />
                  </w:rPr>
                  <w:fldChar w:fldCharType="begin" />
                </w:r>
                <w:r w:rsidRPr="004127E4">
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="16" />
                    <w:szCs w:val="16" />
                  </w:rPr>
                  <w:instrText xml:space="preserve"> PAGE   \* MERGEFORMAT </w:instrText>
                </w:r>
                <w:r w:rsidRPr="004127E4">
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="16" />
                    <w:szCs w:val="16" />
                    <w:noProof />
                  </w:rPr>
                  <w:fldChar w:fldCharType="separate" />
                  <w:t>1</w:t>
                </w:r>
                <w:r w:rsidR="00E401FB">
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:noProof />
                    <w:sz w:val="16" />
                    <w:szCs w:val="16" />
                  </w:rPr>
                  <w:t>1</w:t>
                </w:r>
                <w:r w:rsidRPr="004127E4">
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="16" />
                    <w:szCs w:val="16" />
                  </w:rPr>
                  <w:fldChar w:fldCharType="end" />
                </w:r>
                <w:r w:rsidRPr="004127E4">
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="16" />
                    <w:szCs w:val="16" />
                  </w:rPr>
                  <w:t xml:space="preserve"> de </w:t>
                </w:r>
                <w:fldSimple w:instr=" SECTIONPAGES   \* MERGEFORMAT ">
                  <w:r w:rsidR="00E401FB">
                    <w:rPr>
                      <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                      <w:noProof />
                      <w:sz w:val="16" />
                      <w:szCs w:val="16" />
                    </w:rPr>
                    <w:t>2</w:t>
                  </w:r>
                </w:fldSimple>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="90" w:type="dxa" />
                <w:tcBorders>
                  <w:top w:val="nil" />
                  <w:bottom w:val="nil" />
                </w:tcBorders>
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00507449" w:rsidR="00010551" w:rsidRDefault="00010551">
                <w:pPr>
                  <w:pStyle w:val="Piedepgina" />
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                </w:pPr>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="1216" w:type="dxa" />
                <w:tcBorders>
                  <w:bottom w:val="nil" />
                  <w:right w:val="nil" />
                </w:tcBorders>
                <w:tcMar>
                  <w:right w:w="113" w:type="dxa" />
                </w:tcMar>
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00507449" w:rsidR="00010551" w:rsidP="00507449" w:rsidRDefault="00010551">
                <w:pPr>
                  <w:pStyle w:val="Piedepgina" />
                  <w:jc w:val="right" />
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="16" />
                    <w:szCs w:val="16" />
                  </w:rPr>
                </w:pPr>
                <w:r w:rsidRPr="00507449">
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="16" />
                    <w:szCs w:val="16" />
                  </w:rPr>
                  <w:t>Doctor</w:t>
                </w:r>
                <w:r>
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="16" />
                    <w:szCs w:val="16" />
                  </w:rPr>
                  <w:t>:</w:t>
                </w:r>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="2322" w:type="dxa" />
                <w:tcBorders>
                  <w:left w:val="nil" />
                  <w:bottom w:val="nil" />
                </w:tcBorders>
                <w:tcMar>
                  <w:right w:w="113" w:type="dxa" />
                </w:tcMar>
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00507449" w:rsidR="00010551" w:rsidRDefault="00010551">
                <w:pPr>
                  <w:pStyle w:val="Piedepgina" />
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                </w:pPr>
                <w:r>
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                  <w:t>
                    <xsl:value-of select="PhysicianName" />
                  </w:t>
                </w:r>
              </w:p>
            </w:tc>
          </w:tr>
          <w:tr w:rsidRPr="00507449" w:rsidR="00010551" w:rsidTr="00010551">
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="1722" w:type="dxa" />
                <w:vMerge />
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00507449" w:rsidR="00010551" w:rsidRDefault="00010551">
                <w:pPr>
                  <w:pStyle w:val="Piedepgina" />
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                </w:pPr>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="91" w:type="dxa" />
                <w:vMerge />
                <w:tcBorders>
                  <w:top w:val="nil" />
                  <w:bottom w:val="nil" />
                </w:tcBorders>
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00507449" w:rsidR="00010551" w:rsidRDefault="00010551">
                <w:pPr>
                  <w:pStyle w:val="Piedepgina" />
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                </w:pPr>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="3293" w:type="dxa" />
                <w:vMerge />
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00507449" w:rsidR="00010551" w:rsidRDefault="00010551">
                <w:pPr>
                  <w:pStyle w:val="Piedepgina" />
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                </w:pPr>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="90" w:type="dxa" />
                <w:tcBorders>
                  <w:top w:val="nil" />
                  <w:bottom w:val="nil" />
                </w:tcBorders>
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00507449" w:rsidR="00010551" w:rsidRDefault="00010551">
                <w:pPr>
                  <w:pStyle w:val="Piedepgina" />
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                </w:pPr>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="1216" w:type="dxa" />
                <w:tcBorders>
                  <w:top w:val="nil" />
                  <w:bottom w:val="nil" />
                  <w:right w:val="nil" />
                </w:tcBorders>
                <w:tcMar>
                  <w:right w:w="113" w:type="dxa" />
                </w:tcMar>
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00507449" w:rsidR="00010551" w:rsidP="00507449" w:rsidRDefault="00010551">
                <w:pPr>
                  <w:pStyle w:val="Piedepgina" />
                  <w:jc w:val="right" />
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="16" />
                    <w:szCs w:val="16" />
                  </w:rPr>
                </w:pPr>
                <w:r w:rsidRPr="00507449">
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="16" />
                    <w:szCs w:val="16" />
                  </w:rPr>
                  <w:t>Nº Colegiado</w:t>
                </w:r>
                <w:r>
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="16" />
                    <w:szCs w:val="16" />
                  </w:rPr>
                  <w:t>:</w:t>
                </w:r>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="2322" w:type="dxa" />
                <w:tcBorders>
                  <w:top w:val="nil" />
                  <w:left w:val="nil" />
                  <w:bottom w:val="nil" />
                </w:tcBorders>
                <w:tcMar>
                  <w:right w:w="113" w:type="dxa" />
                </w:tcMar>
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00507449" w:rsidR="00010551" w:rsidRDefault="00010551">
                <w:pPr>
                  <w:pStyle w:val="Piedepgina" />
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                </w:pPr>
                <w:r>
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                  <w:t>
                    <xsl:value-of select="CollegiateNumber" />
                  </w:t>
                </w:r>
              </w:p>
            </w:tc>
          </w:tr>
          <w:tr w:rsidRPr="00507449" w:rsidR="00010551" w:rsidTr="00010551">
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="1722" w:type="dxa" />
                <w:vMerge />
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00507449" w:rsidR="00010551" w:rsidRDefault="00010551">
                <w:pPr>
                  <w:pStyle w:val="Piedepgina" />
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                </w:pPr>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="91" w:type="dxa" />
                <w:vMerge />
                <w:tcBorders>
                  <w:top w:val="nil" />
                  <w:bottom w:val="nil" />
                </w:tcBorders>
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00507449" w:rsidR="00010551" w:rsidRDefault="00010551">
                <w:pPr>
                  <w:pStyle w:val="Piedepgina" />
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                </w:pPr>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="3293" w:type="dxa" />
                <w:vMerge />
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00507449" w:rsidR="00010551" w:rsidRDefault="00010551">
                <w:pPr>
                  <w:pStyle w:val="Piedepgina" />
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                </w:pPr>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="90" w:type="dxa" />
                <w:tcBorders>
                  <w:top w:val="nil" />
                  <w:bottom w:val="nil" />
                </w:tcBorders>
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00507449" w:rsidR="00010551" w:rsidRDefault="00010551">
                <w:pPr>
                  <w:pStyle w:val="Piedepgina" />
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                </w:pPr>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="1216" w:type="dxa" />
                <w:tcBorders>
                  <w:top w:val="nil" />
                  <w:right w:val="nil" />
                </w:tcBorders>
                <w:tcMar>
                  <w:right w:w="113" w:type="dxa" />
                </w:tcMar>
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00507449" w:rsidR="00010551" w:rsidP="00507449" w:rsidRDefault="00010551">
                <w:pPr>
                  <w:pStyle w:val="Piedepgina" />
                  <w:jc w:val="right" />
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="16" />
                    <w:szCs w:val="16" />
                  </w:rPr>
                </w:pPr>
                <w:r w:rsidRPr="00507449">
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="16" />
                    <w:szCs w:val="16" />
                  </w:rPr>
                  <w:t>Servicio</w:t>
                </w:r>
                <w:r>
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="16" />
                    <w:szCs w:val="16" />
                  </w:rPr>
                  <w:t>:</w:t>
                </w:r>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="2322" w:type="dxa" />
                <w:tcBorders>
                  <w:top w:val="nil" />
                  <w:left w:val="nil" />
                </w:tcBorders>
                <w:tcMar>
                  <w:right w:w="113" w:type="dxa" />
                </w:tcMar>
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00507449" w:rsidR="00010551" w:rsidRDefault="00010551">
                <w:pPr>
                  <w:pStyle w:val="Piedepgina" />
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                </w:pPr>
                <w:r>
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                  <w:t>
                    <xsl:value-of select="AssistanceServiceName" />
                  </w:t>
                </w:r>
              </w:p>
            </w:tc>
          </w:tr>
        </w:tbl>
        <w:p w:rsidRPr="00507449" w:rsidR="00D52016" w:rsidP="00507449" w:rsidRDefault="00507449">
          <w:pPr>
            <w:pStyle w:val="Piedepgina" />
            <w:jc w:val="center" />
            <w:rPr>
              <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
              <w:color w:val="7F7F7F" w:themeColor="text1" w:themeTint="80" />
              <w:sz w:val="10" />
              <w:szCs w:val="10" />
            </w:rPr>
          </w:pPr>
          <w:r w:rsidRPr="00507449">
            <w:rPr>
              <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
              <w:color w:val="7F7F7F" w:themeColor="text1" w:themeTint="80" />
              <w:sz w:val="10" />
              <w:szCs w:val="10" />
            </w:rPr>
            <w:t>
              <xsl:value-of select="Copyright" />
            </w:t>
          </w:r>
        </w:p>
      </w:ftr>
      <!-- End footer -->
    </wordDoc>
  </xsl:template>
  <!-- End CustomerReportTemplate -->

  <!-- Start ReportTemplate -->
  <xsl:template  match="ReportTemplates/ReportTemplateDTO" xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main">
    <w:tbl>
      <w:tblPr>
        <w:tblStyle w:val="Tablaconcuadrcula" />
        <w:tblW w:w="8897" w:type="dxa" />
        <w:tblLook w:val="04A0" />
      </w:tblPr>
      <w:tblGrid>
        <w:gridCol w:w="250" />
        <w:gridCol w:w="5612" />
        <w:gridCol w:w="3050" />
      </w:tblGrid>
      <w:tr w:rsidR="00BB5832" w:rsidTr="00E401FB">
        <w:tc>
          <w:tcPr>
            <w:tcW w:w="6345" w:type="dxa" />
            <w:gridSpan w:val="2" />
          </w:tcPr>
          <w:p w:rsidRPr="00E401FB" w:rsidR="00BB5832" w:rsidRDefault="00E401FB">
            <w:pPr>
              <w:rPr>
                <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                <w:b />
                <w:sz w:val="18" />
                <w:szCs w:val="18" />
              </w:rPr>
            </w:pPr>
            <w:r w:rsidRPr="00E401FB">
              <w:rPr>
                <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                <w:b />
                <w:sz w:val="18" />
                <w:szCs w:val="18" />
              </w:rPr>
              <w:t>
                <xsl:value-of select="Title" />
              </w:t>
            </w:r>
          </w:p>
        </w:tc>
        <w:tc>
          <w:tcPr>
            <w:tcW w:w="2552" w:type="dxa" />
            <w:vAlign w:val="right" />
          </w:tcPr>
          <w:p w:rsidRPr="00BB5832" w:rsidR="00BB5832" w:rsidRDefault="00BB5832">
            <w:pPr>
              <w:rPr>
                <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                <w:sz w:val="18" />
                <w:szCs w:val="18" />
              </w:rPr>
            </w:pPr>
            <w:r w:rsidRPr="00BB5832">
              <w:rPr>
                <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                <w:sz w:val="18" />
                <w:szCs w:val="18" />
              </w:rPr>
              <!--<w:t>Fecha: </w:t>-->
              <w:t>
                <xsl:value-of select="DateTime" />
              </w:t>
            </w:r>
          </w:p>
        </w:tc>
      </w:tr>
      <xsl:apply-templates select="Blocks"/>
    </w:tbl>

    <w:p w:rsidR="00E401FB" w:rsidRDefault="00E401FB" />
    <xsl:if test="position()!=last()">
      <xsl:if test="PageBreakAfterTemplate='true'">
        <w:p w:rsidR="003F55CB" w:rsidRDefault="003F55CB">
          <w:r>
            <w:lastRenderedPageBreak />
            <w:br w:type="page" />
          </w:r>
        </w:p>
        <w:p w:rsidR="00615B53" w:rsidP="00615B53" w:rsidRDefault="00615B53">
          <w:pPr>
            <w:sectPr w:rsidR="00615B53" w:rsidSect="003F55CB">
              <w:headerReference w:type="default" r:id="rId7" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships" />
              <w:pgSz w:w="11906" w:h="16838" />
              <w:pgMar w:top="1417" w:right="1701" w:bottom="1417" w:left="1701" w:header="708" w:footer="708" w:gutter="0" />
              <w:pgNumType w:start="1" />
              <w:cols w:space="708" />
              <w:docGrid w:linePitch="360" />
            </w:sectPr>
          </w:pPr>
        </w:p>
        <w:p w:rsidRPr="00615B53" w:rsidR="00C34E85" w:rsidP="00615B53" w:rsidRDefault="00C34E85" />
        <w:sectPr w:rsidRPr="00615B53" w:rsidR="00C34E85" w:rsidSect="003F55CB">
          <w:pgSz w:w="11906" w:h="16838" />
          <w:pgMar w:top="1417" w:right="1701" w:bottom="1417" w:left="1701" w:header="708" w:footer="708" w:gutter="0" />
          <w:pgNumType w:start="1" />
          <w:cols w:space="708" />
          <w:docGrid w:linePitch="360" />
        </w:sectPr>
      </xsl:if>
    </xsl:if>
  </xsl:template>
  <!-- End ReportTemplate -->

  <!-- Start ReportBlock -->
  <xsl:template match="Blocks/ReportBlockDTO" xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main">
    <w:tr w:rsidR="00BB5832" w:rsidTr="00E401FB">
      <w:tc>
        <w:tcPr>
          <w:tcW w:w="250" w:type="dxa" />
        </w:tcPr>
        <w:p w:rsidRPr="00E401FB" w:rsidR="00BB5832" w:rsidRDefault="00BB5832">
          <w:pPr>
            <w:rPr>
              <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
              <w:sz w:val="18" />
              <w:szCs w:val="18" />
            </w:rPr>
          </w:pPr>
        </w:p>
      </w:tc>
      <w:tc>
        <w:tcPr>
          <w:tcW w:w="8647" w:type="dxa" />
          <w:gridSpan w:val="2" />
          <w:tcMar>
            <w:left w:w="0" w:type="dxa" />
            <w:right w:w="0" w:type="dxa" />
          </w:tcMar>
        </w:tcPr>
        <w:tbl>
          <w:tblPr>
            <w:tblStyle w:val="Tablaconcuadrcula" />
            <w:tblW w:w="8647" w:type="dxa" />
            <w:tblBorders>
              <w:top w:val="none" w:color="auto" w:sz="0" w:space="0" />
              <w:left w:val="none" w:color="auto" w:sz="0" w:space="0" />
              <w:bottom w:val="none" w:color="auto" w:sz="0" w:space="0" />
            </w:tblBorders>
            <w:tblLook w:val="04A0" />
          </w:tblPr>
          <w:tblGrid>
            <w:gridCol w:w="222" />
            <w:gridCol w:w="8425" />
          </w:tblGrid>
          <w:tr w:rsidRPr="00E401FB" w:rsidR="00E401FB" w:rsidTr="00E401FB">
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="8647" w:type="dxa" />
                <w:gridSpan w:val="2" />
                <w:tcMar>
                  <w:left w:w="108" w:type="dxa" />
                  <w:right w:w="108" w:type="dxa" />
                </w:tcMar>
              </w:tcPr>
              <w:p w:rsidRPr="00E401FB" w:rsidR="00E401FB" w:rsidRDefault="00E401FB">
                <w:pPr>
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:b />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                </w:pPr>
                <w:r w:rsidRPr="00E401FB">
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:b />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                  <w:t>
                    <xsl:value-of select="Title"/>
                  </w:t>
                </w:r>
              </w:p>
            </w:tc>
          </w:tr>

          <w:tr w:rsidRPr="00E401FB" w:rsidR="00BB5832" w:rsidTr="00E401FB">
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="222" w:type="dxa" />
                <w:tcMar>
                  <w:left w:w="108" w:type="dxa" />
                  <w:right w:w="108" w:type="dxa" />
                </w:tcMar>
              </w:tcPr>
              <w:p w:rsidRPr="00E401FB" w:rsidR="00BB5832" w:rsidRDefault="00BB5832">
                <w:pPr>
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                </w:pPr>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="8425" w:type="dxa" />
                <w:tcMar>
                  <w:left w:w="0" w:type="dxa" />
                  <w:right w:w="0" w:type="dxa" />
                </w:tcMar>
              </w:tcPr>
              <w:tbl>
                <w:tblPr>
                  <w:tblStyle w:val="Tablaconcuadrcula" />
                  <w:tblW w:w="0" w:type="auto" />
                  <w:tblBorders>
                    <w:top w:val="none" w:color="auto" w:sz="0" w:space="0" />
                    <w:left w:val="none" w:color="auto" w:sz="0" w:space="0" />
                    <w:bottom w:val="none" w:color="auto" w:sz="0" w:space="0" />
                    <w:insideH w:val="none" w:color="auto" w:sz="0" w:space="0" />
                    <!--<w:insideV w:val="none" w:color="auto" w:sz="0" w:space="0" />-->
                  </w:tblBorders>
                  <w:tblLook w:val="04A0" />
                </w:tblPr>
                <w:tblGrid>
                  <w:gridCol w:w="1733" />
                  <w:gridCol w:w="1735" />
                  <w:gridCol w:w="1733" />
                  <w:gridCol w:w="1735" />
                </w:tblGrid>
                <xsl:apply-templates select="BlockRows"/>
              </w:tbl>
              <w:p w:rsidRPr="00E401FB" w:rsidR="00BB5832" w:rsidRDefault="00BB5832">
                <w:pPr>
                  <w:rPr>
                    <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                </w:pPr>
              </w:p>
            </w:tc>
          </w:tr>
        </w:tbl>
        <w:p w:rsidRPr="00E401FB" w:rsidR="00BB5832" w:rsidRDefault="00BB5832">
          <w:pPr>
            <w:rPr>
              <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
              <w:sz w:val="18" />
              <w:szCs w:val="18" />
            </w:rPr>
          </w:pPr>
        </w:p>
      </w:tc>
    </w:tr>
  </xsl:template>
  <!-- End ReportBlock -->

  <!-- Start ReportObservationRow -->
  <xsl:template match="BlockRows/ReportBlockRowDTO" xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main">
    <w:tr w:rsidRPr="00E401FB" w:rsidR="00E401FB" w:rsidTr="00E401FB">
      <xsl:apply-templates select="Observations"/>
    </w:tr>
  </xsl:template>
  <!-- End ReportObservationRow-->

  <!-- Start ReportObservation -->
  <xsl:template match="Observations/ReportObservationDTO" xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main">
    <w:tc>
      <w:tcPr>
        <w:tcW w:w="0" w:type="auto" />
        <xsl:if test="ColSpan>'1'">
          <w:gridSpan>
            <xsl:attribute name="w:val">
              <xsl:value-of select="ColSpan"/>
            </xsl:attribute>
          </w:gridSpan>
        </xsl:if>
        <w:vAlign w:val="center" />
      </w:tcPr>
      <w:p w:rsidRPr="00E401FB" w:rsidR="00E401FB" w:rsidP="00E401FB" w:rsidRDefault="00E401FB">
        <w:pPr>
          <w:rPr>
            <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
            <w:sz w:val="16" />
            <w:szCs w:val="16" />
          </w:rPr>
        </w:pPr>
        <w:r>
          <w:rPr>
            <w:rFonts w:asciiTheme="majorHAnsi" w:hAnsiTheme="majorHAnsi" />
            <w:sz w:val="16" />
            <w:szCs w:val="16" />
          </w:rPr>
          <w:t>
            <xsl:choose>
              <xsl:when  test="Type='Label'">
                <xsl:value-of select="Value"/>
              </xsl:when>
              <xsl:otherwise>
                <xsl:choose>
                  <xsl:when test="ValueType='RichText'">
                    <xsl:text>&lt;html&gt;&lt;body&gt;</xsl:text>
                    <xsl:value-of select="Value"/>
                    <xsl:text>&lt;/body&gt;&lt;/html&gt;</xsl:text>
                  </xsl:when>
                  <xsl:when test="ValueType='Boolean'">
                    <xsl:if test="Value='true'">
                      <xsl:text>X</xsl:text>
                    </xsl:if>
                  </xsl:when>
                  <xsl:when test="ValueType='Image'">
                    <xsl:text>&lt;html&gt;&lt;body&gt;&lt;img src='</xsl:text>
                    <xsl:value-of select="Value"/>
                    <xsl:text>'/&gt;&lt;/body&gt;&lt;/html&gt;</xsl:text>
                  </xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select="Value"/>
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:otherwise>
            </xsl:choose>
          </w:t>
        </w:r>
      </w:p>
    </w:tc>
  </xsl:template>
  <!-- End ReportObservation-->
</xsl:stylesheet>
