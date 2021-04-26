<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <xsl:output method="xml" indent="yes"/>

  <xsl:template match="/">
    <xsl:apply-templates select="CustomerReportTemplateDTO"/>
  </xsl:template>

  <xsl:template match="CustomerReportTemplateDTO">
    <wordDoc>
      <!--Sustituir-->
      <!-- Inicio cabecera-->
      <w:hdr xmlns:a="http://schemas.openxmlformats.org/drawingml/2006/main" 
             xmlns:pic="http://schemas.openxmlformats.org/drawingml/2006/picture" 
             xmlns:ve="http://schemas.openxmlformats.org/markup-compatibility/2006" 
             xmlns:o="urn:schemas-microsoft-com:office:office" 
             xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships" 
             xmlns:m="http://schemas.openxmlformats.org/officeDocument/2006/math" 
             xmlns:v="urn:schemas-microsoft-com:vml" 
             xmlns:wp="http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing" 
             xmlns:w10="urn:schemas-microsoft-com:office:word" 
             xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main" 
             xmlns:wne="http://schemas.microsoft.com/office/word/2006/wordml">
        <w:tbl>
          <w:tblPr>
            <w:tblW w:w="5000" w:type="pct" />
            <w:tblBorders>
              <w:top w:val="single" w:color="DBE5F1" w:themeColor="accent1" w:themeTint="33" w:sz="12" w:space="0" />
              <w:left w:val="single" w:color="DBE5F1" w:themeColor="accent1" w:themeTint="33" w:sz="12" w:space="0" />
              <w:bottom w:val="single" w:color="DBE5F1" w:themeColor="accent1" w:themeTint="33" w:sz="12" w:space="0" />
              <w:right w:val="single" w:color="DBE5F1" w:themeColor="accent1" w:themeTint="33" w:sz="12" w:space="0" />
            </w:tblBorders>
            <w:shd w:val="clear" w:color="auto" w:fill="DBE5F1" w:themeFill="accent1" w:themeFillTint="33" />
            <w:tblLook w:val="04A0" />
          </w:tblPr>
          <w:tblGrid>
            <w:gridCol w:w="1306" />
            <w:gridCol w:w="3479" />
            <w:gridCol w:w="1277" />
            <w:gridCol w:w="2658" />
          </w:tblGrid>
          <w:tr w:rsidR="00131405" w:rsidTr="00724DBF">
            <w:trPr>
              <w:trHeight w:val="143" />
            </w:trPr>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="749" w:type="pct" />
                <w:vMerge w:val="restart" />
                <w:shd w:val="clear" w:color="auto" w:fill="DBE5F1" w:themeFill="accent1" w:themeFillTint="33" />
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00E83BF1" w:rsidR="00131405" w:rsidP="00724DBF" w:rsidRDefault="00CE4A54">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:jc w:val="center" />
                  <w:rPr>
                    <w:b />
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                </w:pPr>
                <w:r w:rsidRPr="00E83BF1">
                  <w:rPr>
                    <w:b />
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                  <w:t>
                    <xsl:text>&lt;html&gt;&lt;body&gt;&lt;img src="CompanyLogo.png" width="100" height = "100" /&gt;&lt;/body&gt;&lt;/html&gt;</xsl:text>
                  </w:t>
                </w:r>

                <!--<w:r>
                  <w:rPr>
                    <w:lang w:val="es-ES" />
                  </w:rPr>
                  -->
                <!--<w:drawing>
                    <wp:inline distT="0" distB="0" distL="0" distR="0">
                      <wp:extent cx="609739" cy="609739" />
                      <wp:effectExtent l="19050" t="0" r="0" b="0" />
                      <wp:docPr id="1" name="0 Imagen" descr="medical_bag.png" />
                      <wp:cNvGraphicFramePr>
                        <a:graphicFrameLocks xmlns:a="http://schemas.openxmlformats.org/drawingml/2006/main" noChangeAspect="1" />
                      </wp:cNvGraphicFramePr>
                      <a:graphic xmlns:a="http://schemas.openxmlformats.org/drawingml/2006/main">
                        <a:graphicData uri="http://schemas.openxmlformats.org/drawingml/2006/picture">
                          <pic:pic xmlns:pic="http://schemas.openxmlformats.org/drawingml/2006/picture">
                            <pic:nvPicPr>
                              <pic:cNvPr id="0" name="medical_bag.png" />
                              <pic:cNvPicPr />
                            </pic:nvPicPr>
                            <pic:blipFill>
                              <a:blip r:embed="rId1" />
                              <a:stretch>
                                <a:fillRect />
                              </a:stretch>
                            </pic:blipFill>
                            <pic:spPr>
                              <a:xfrm>
                                <a:off x="0" y="0" />
                                <a:ext cx="609739" cy="609739" />
                              </a:xfrm>
                              <a:prstGeom prst="rect">
                                <a:avLst />
                              </a:prstGeom>
                            </pic:spPr>
                          </pic:pic>
                        </a:graphicData>
                      </a:graphic>
                    </wp:inline>
                  </w:drawing>-->
                <!--
                </w:r>-->
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="1995" w:type="pct" />
                <w:vMerge w:val="restart" />
                <w:shd w:val="clear" w:color="auto" w:fill="DBE5F1" w:themeFill="accent1" w:themeFillTint="33" />
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00E83BF1" w:rsidR="00131405" w:rsidP="00724DBF" w:rsidRDefault="00CE4A54">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:jc w:val="center" />
                  <w:rPr>
                    <w:b />
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                    <w:sz w:val="32" />
                    <w:szCs w:val="32" />
                  </w:rPr>
                </w:pPr>
                <w:r>
                  <w:rPr>
                    <w:b />
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                    <w:sz w:val="32" />
                    <w:szCs w:val="32" />
                  </w:rPr>
                  <w:t>
                    <xsl:value-of select="ReportTitle" />
                  </w:t>
                </w:r>
              </w:p>
              <w:p w:rsidRPr="00E83BF1" w:rsidR="00131405" w:rsidP="00724DBF" w:rsidRDefault="00131405">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:jc w:val="center" />
                  <w:rPr>
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                    <w:sz w:val="16" />
                    <w:szCs w:val="16" />
                  </w:rPr>
                </w:pPr>
                <w:r w:rsidRPr="00E83BF1">
                  <w:rPr>
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                    <w:sz w:val="16" />
                    <w:szCs w:val="16" />
                  </w:rPr>
                  <w:t>Fecha de informe: </w:t>
                  <w:t>
                    <xsl:value-of select="ReportDateTime" />
                  </w:t>
                </w:r>
              </w:p>
              <w:p w:rsidRPr="00E83BF1" w:rsidR="00131405" w:rsidP="00724DBF" w:rsidRDefault="00131405">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:jc w:val="center" />
                  <w:rPr>
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                    <w:sz w:val="16" />
                    <w:szCs w:val="16" />
                  </w:rPr>
                </w:pPr>
                <w:r w:rsidRPr="00E83BF1">
                  <w:rPr>
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                    <w:sz w:val="16" />
                    <w:szCs w:val="16" />
                  </w:rPr>
                  <w:t xml:space="preserve">Página </w:t>
                </w:r>
                <w:r w:rsidRPr="00E83BF1" w:rsidR="00A01F00">
                  <w:rPr>
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                    <w:sz w:val="16" />
                    <w:szCs w:val="16" />
                  </w:rPr>
                  <w:fldChar w:fldCharType="begin" />
                </w:r>
                <w:r w:rsidRPr="00E83BF1">
                  <w:rPr>
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                    <w:sz w:val="16" />
                    <w:szCs w:val="16" />
                  </w:rPr>
                  <w:instrText xml:space="preserve"> PAGE   \* MERGEFORMAT </w:instrText>
                </w:r>
                <w:r w:rsidRPr="00E83BF1" w:rsidR="00A01F00">
                  <w:rPr>
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                    <w:sz w:val="16" />
                    <w:szCs w:val="16" />
                    <w:noProof />
                  </w:rPr>
                  <w:fldChar w:fldCharType="separate" />
                  <w:t>1</w:t>
                </w:r>
                <w:r w:rsidR="000A31EB">
                  <w:rPr>
                    <w:noProof />
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                    <w:sz w:val="16" />
                    <w:szCs w:val="16" />
                  </w:rPr>
                  <w:t>1</w:t>
                </w:r>
                <w:r w:rsidRPr="00E83BF1" w:rsidR="00A01F00">
                  <w:rPr>
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                    <w:sz w:val="16" />
                    <w:szCs w:val="16" />
                  </w:rPr>
                  <w:fldChar w:fldCharType="end" />
                </w:r>
                <w:r w:rsidRPr="00E83BF1">
                  <w:rPr>
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                    <w:sz w:val="16" />
                    <w:szCs w:val="16" />
                  </w:rPr>
                  <w:t xml:space="preserve"> de </w:t>
                </w:r>
                <w:fldSimple w:instr=" SECTIONPAGES  \* Arabic  \* MERGEFORMAT ">
                  <w:r w:rsidRPr="000A31EB" w:rsidR="000A31EB">
                    <w:rPr>
                      <w:noProof />
                      <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
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
                <w:tcW w:w="732" w:type="pct" />
                <w:shd w:val="clear" w:color="auto" w:fill="DBE5F1" w:themeFill="accent1" w:themeFillTint="33" />
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00E83BF1" w:rsidR="00131405" w:rsidP="00724DBF" w:rsidRDefault="00131405">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:jc w:val="right" />
                  <w:rPr>
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                </w:pPr>
                <w:r w:rsidRPr="00E83BF1">
                  <w:rPr>
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                  <w:t>Nº Episodio</w:t>
                </w:r>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="1524" w:type="pct" />
                <w:tcBorders>
                  <w:top w:val="single" w:color="DBE5F1" w:themeColor="accent1" w:themeTint="33" w:sz="12" w:space="0" />
                  <w:bottom w:val="nil" />
                </w:tcBorders>
                <w:shd w:val="clear" w:color="auto" w:fill="FFFFFF" w:themeFill="background1" />
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00E83BF1" w:rsidR="00131405" w:rsidP="00724DBF" w:rsidRDefault="00131405">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:rPr>
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                </w:pPr>
                <w:r w:rsidRPr="00E83BF1">
                  <w:rPr>
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
          <w:tr w:rsidR="00131405" w:rsidTr="00724DBF">
            <w:trPr>
              <w:trHeight w:val="143" />
            </w:trPr>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="749" w:type="pct" />
                <w:vMerge />
                <w:shd w:val="clear" w:color="auto" w:fill="DBE5F1" w:themeFill="accent1" w:themeFillTint="33" />
              </w:tcPr>
              <w:p w:rsidRPr="00E83BF1" w:rsidR="00131405" w:rsidP="00724DBF" w:rsidRDefault="00131405">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:rPr>
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                  </w:rPr>
                </w:pPr>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="1995" w:type="pct" />
                <w:vMerge />
                <w:shd w:val="clear" w:color="auto" w:fill="DBE5F1" w:themeFill="accent1" w:themeFillTint="33" />
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00E83BF1" w:rsidR="00131405" w:rsidP="00724DBF" w:rsidRDefault="00131405">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:rPr>
                    <w:caps />
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                  </w:rPr>
                </w:pPr>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="732" w:type="pct" />
                <w:shd w:val="clear" w:color="auto" w:fill="DBE5F1" w:themeFill="accent1" w:themeFillTint="33" />
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00E83BF1" w:rsidR="00131405" w:rsidP="00724DBF" w:rsidRDefault="00131405">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:jc w:val="right" />
                  <w:rPr>
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                </w:pPr>
                <w:r w:rsidRPr="00E83BF1">
                  <w:rPr>
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                  <w:t>Nº Historia</w:t>
                </w:r>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="1524" w:type="pct" />
                <w:tcBorders>
                  <w:top w:val="nil" />
                  <w:bottom w:val="nil" />
                </w:tcBorders>
                <w:shd w:val="clear" w:color="auto" w:fill="FFFFFF" w:themeFill="background1" />
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00E83BF1" w:rsidR="00131405" w:rsidP="00724DBF" w:rsidRDefault="00131405">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:rPr>
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                </w:pPr>
                <w:r w:rsidRPr="00E83BF1">
                  <w:rPr>
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
          <w:tr w:rsidR="00131405" w:rsidTr="00724DBF">
            <w:trPr>
              <w:trHeight w:val="143" />
            </w:trPr>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="749" w:type="pct" />
                <w:vMerge />
                <w:shd w:val="clear" w:color="auto" w:fill="DBE5F1" w:themeFill="accent1" w:themeFillTint="33" />
              </w:tcPr>
              <w:p w:rsidRPr="00E83BF1" w:rsidR="00131405" w:rsidP="00724DBF" w:rsidRDefault="00131405">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:rPr>
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                  </w:rPr>
                </w:pPr>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="1995" w:type="pct" />
                <w:vMerge />
                <w:shd w:val="clear" w:color="auto" w:fill="DBE5F1" w:themeFill="accent1" w:themeFillTint="33" />
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00E83BF1" w:rsidR="00131405" w:rsidP="00724DBF" w:rsidRDefault="00131405">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:rPr>
                    <w:caps />
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                  </w:rPr>
                </w:pPr>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="732" w:type="pct" />
                <w:shd w:val="clear" w:color="auto" w:fill="DBE5F1" w:themeFill="accent1" w:themeFillTint="33" />
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00E83BF1" w:rsidR="00131405" w:rsidP="00724DBF" w:rsidRDefault="00131405">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:jc w:val="right" />
                  <w:rPr>
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                </w:pPr>
                <w:r w:rsidRPr="00E83BF1">
                  <w:rPr>
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                  <w:t>Nombre</w:t>
                </w:r>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="1524" w:type="pct" />
                <w:tcBorders>
                  <w:top w:val="nil" />
                  <w:bottom w:val="nil" />
                </w:tcBorders>
                <w:shd w:val="clear" w:color="auto" w:fill="FFFFFF" w:themeFill="background1" />
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00E83BF1" w:rsidR="00131405" w:rsidP="00724DBF" w:rsidRDefault="00131405">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:rPr>
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                </w:pPr>
                <w:r w:rsidRPr="00E83BF1">
                  <w:rPr>
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
          <w:tr w:rsidR="00131405" w:rsidTr="00724DBF">
            <w:trPr>
              <w:trHeight w:val="143" />
            </w:trPr>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="749" w:type="pct" />
                <w:vMerge />
                <w:shd w:val="clear" w:color="auto" w:fill="DBE5F1" w:themeFill="accent1" w:themeFillTint="33" />
              </w:tcPr>
              <w:p w:rsidRPr="00E83BF1" w:rsidR="00131405" w:rsidP="00724DBF" w:rsidRDefault="00131405">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:rPr>
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                  </w:rPr>
                </w:pPr>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="1995" w:type="pct" />
                <w:vMerge />
                <w:shd w:val="clear" w:color="auto" w:fill="DBE5F1" w:themeFill="accent1" w:themeFillTint="33" />
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00E83BF1" w:rsidR="00131405" w:rsidP="00724DBF" w:rsidRDefault="00131405">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:rPr>
                    <w:caps />
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                  </w:rPr>
                </w:pPr>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="732" w:type="pct" />
                <w:shd w:val="clear" w:color="auto" w:fill="DBE5F1" w:themeFill="accent1" w:themeFillTint="33" />
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00E83BF1" w:rsidR="00131405" w:rsidP="00724DBF" w:rsidRDefault="00131405">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:jc w:val="right" />
                  <w:rPr>
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                </w:pPr>
                <w:r w:rsidRPr="00E83BF1">
                  <w:rPr>
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                  <w:t>Fecha</w:t>
                </w:r>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="1524" w:type="pct" />
                <w:tcBorders>
                  <w:top w:val="nil" />
                  <w:bottom w:val="nil" />
                </w:tcBorders>
                <w:shd w:val="clear" w:color="auto" w:fill="FFFFFF" w:themeFill="background1" />
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00E83BF1" w:rsidR="00131405" w:rsidP="00724DBF" w:rsidRDefault="00131405">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:rPr>
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                </w:pPr>
                <w:r w:rsidRPr="00E83BF1">
                  <w:rPr>
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
          <w:tr w:rsidR="00131405" w:rsidTr="00724DBF">
            <w:trPr>
              <w:trHeight w:val="143" />
            </w:trPr>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="749" w:type="pct" />
                <w:vMerge />
                <w:shd w:val="clear" w:color="auto" w:fill="DBE5F1" w:themeFill="accent1" w:themeFillTint="33" />
              </w:tcPr>
              <w:p w:rsidRPr="00E83BF1" w:rsidR="00131405" w:rsidP="00724DBF" w:rsidRDefault="00131405">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:rPr>
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                  </w:rPr>
                </w:pPr>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="1995" w:type="pct" />
                <w:vMerge />
                <w:shd w:val="clear" w:color="auto" w:fill="DBE5F1" w:themeFill="accent1" w:themeFillTint="33" />
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00E83BF1" w:rsidR="00131405" w:rsidP="00724DBF" w:rsidRDefault="00131405">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:rPr>
                    <w:caps />
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                  </w:rPr>
                </w:pPr>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="732" w:type="pct" />
                <w:shd w:val="clear" w:color="auto" w:fill="DBE5F1" w:themeFill="accent1" w:themeFillTint="33" />
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00E83BF1" w:rsidR="00131405" w:rsidP="00724DBF" w:rsidRDefault="00131405">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:jc w:val="right" />
                  <w:rPr>
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                </w:pPr>
                <w:r w:rsidRPr="00E83BF1">
                  <w:rPr>
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                  <w:t>Doctor</w:t>
                </w:r>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="1524" w:type="pct" />
                <w:tcBorders>
                  <w:top w:val="nil" />
                  <w:bottom w:val="single" w:color="DBE5F1" w:themeColor="accent1" w:themeTint="33" w:sz="12" w:space="0" />
                </w:tcBorders>
                <w:shd w:val="clear" w:color="auto" w:fill="FFFFFF" w:themeFill="background1" />
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00E83BF1" w:rsidR="00131405" w:rsidP="00724DBF" w:rsidRDefault="00131405">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:rPr>
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                </w:pPr>
                <w:r w:rsidRPr="00E83BF1">
                  <w:rPr>
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
        </w:tbl>


        <w:p w:rsidR="00131405" w:rsidRDefault="000A31EB">
          <w:pPr>
            <w:pStyle w:val="Encabezado" />
          </w:pPr>
          <w:r>
            <w:rPr>
              <w:noProof />
              <w:lang w:eastAsia="es-ES" />
            </w:rPr>
            <w:pict>
              <v:shapetype id="_x0000_t202" coordsize="21600,21600" o:spt="202" path="m,l,21600r21600,l21600,xe">
                <v:stroke joinstyle="miter" />
                <v:path gradientshapeok="t" o:connecttype="rect" />
              </v:shapetype>
              <v:shape id="_x0000_s3074" style="position:absolute;margin-left:-4.2pt;margin-top:2.25pt;width:434.6pt;height:8.05pt;z-index:-251658240;mso-position-horizontal-relative:text;mso-position-vertical-relative:text;mso-width-relative:margin;mso-height-relative:margin;v-text-anchor:middle" filled="f" stroked="f" type="#_x0000_t202">
                <v:textbox inset="0,0,0,0">
                  <w:txbxContent>
                    <w:p w:rsidRPr="000A31EB" w:rsidR="000A31EB" w:rsidP="000A31EB" w:rsidRDefault="000A31EB">
                      <w:pPr>
                        <w:jc w:val="center" />
                        <w:rPr>
                          <w:color w:val="B8CCE4" w:themeColor="accent1" w:themeTint="66" />
                          <w:sz w:val="12" />
                          <w:szCs w:val="12" />
                        </w:rPr>
                      </w:pPr>
                      <w:r w:rsidRPr="000A31EB">
                        <w:rPr>
                          <w:color w:val="B8CCE4" w:themeColor="accent1" w:themeTint="66" />
                          <w:sz w:val="12" />
                          <w:szCs w:val="12" />
                        </w:rPr>
                        <w:t>
                          <xsl:value-of select="Copyright" />
                        </w:t>
                      </w:r>
                    </w:p>
                  </w:txbxContent>
                </v:textbox>
              </v:shape>
            </w:pict>
          </w:r>
        </w:p>
      </w:hdr>
      <!-- Fin cabecera -->

      <!-- Inicio cuerpo-->
      <w:document xmlns:ve="http://schemas.openxmlformats.org/markup-compatibility/2006" xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships" xmlns:m="http://schemas.openxmlformats.org/officeDocument/2006/math" xmlns:v="urn:schemas-microsoft-com:vml" xmlns:wp="http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing" xmlns:w10="urn:schemas-microsoft-com:office:word" xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main" xmlns:wne="http://schemas.microsoft.com/office/word/2006/wordml">
        <w:body>
          <xsl:apply-templates select="ReportTemplates"/>

          <w:sectPr w:rsidR="009529E9" w:rsidSect="009529E9">
            <w:pgSz w:w="11906" w:h="16838" />
            <w:pgMar w:top="1417" w:right="1701" w:bottom="1417" w:left="1701" w:header="708" w:footer="708" w:gutter="0" />
            <w:cols w:space="708" />
            <w:docGrid w:linePitch="360" />
          </w:sectPr>
        </w:body>
      </w:document>
      <!-- Fin cuerpo -->

      <!-- Inicio pie página-->
      <w:ftr xmlns:ve="http://schemas.openxmlformats.org/markup-compatibility/2006" 
             xmlns:o="urn:schemas-microsoft-com:office:office" 
             xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships" 
             xmlns:m="http://schemas.openxmlformats.org/officeDocument/2006/math" 
             xmlns:v="urn:schemas-microsoft-com:vml" 
             xmlns:wp="http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing" 
             xmlns:w10="urn:schemas-microsoft-com:office:word" 
             xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main" 
             xmlns:wne="http://schemas.microsoft.com/office/word/2006/wordml">
        <w:tbl>
          <w:tblPr>
            <w:tblW w:w="5029" w:type="pct" />
            <w:tblBorders>
              <w:top w:val="single" w:color="DBE5F1" w:themeColor="accent1" w:themeTint="33" w:sz="12" w:space="0" />
              <w:left w:val="single" w:color="DBE5F1" w:themeColor="accent1" w:themeTint="33" w:sz="12" w:space="0" />
              <w:bottom w:val="single" w:color="DBE5F1" w:themeColor="accent1" w:themeTint="33" w:sz="12" w:space="0" />
              <w:right w:val="single" w:color="DBE5F1" w:themeColor="accent1" w:themeTint="33" w:sz="12" w:space="0" />
            </w:tblBorders>
            <w:shd w:val="clear" w:color="auto" w:fill="DBE5F1" w:themeFill="accent1" w:themeFillTint="33" />
            <w:tblLook w:val="04A0" />
          </w:tblPr>
          <w:tblGrid>
            <w:gridCol w:w="1313" />
            <w:gridCol w:w="2501" />
            <w:gridCol w:w="1449" />
            <w:gridCol w:w="3508" />
          </w:tblGrid>
          <w:tr w:rsidR="00131405" w:rsidTr="00724DBF">
            <w:trPr>
              <w:trHeight w:val="226" />
            </w:trPr>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="748" w:type="pct" />
                <w:vMerge w:val="restart" />
                <w:shd w:val="clear" w:color="auto" w:fill="DBE5F1" w:themeFill="accent1" w:themeFillTint="33" />
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00E83BF1" w:rsidR="00131405" w:rsidP="00724DBF" w:rsidRDefault="00131405">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:jc w:val="center" />
                  <w:rPr>
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                    <w:sz w:val="2" />
                    <w:szCs w:val="2" />
                  </w:rPr>
                </w:pPr>
                <w:r w:rsidRPr="00E83BF1">
                  <w:rPr>
                    <w:sz w:val="2" />
                    <w:szCs w:val="2" />
                  </w:rPr>
                  <w:t>
                    <xsl:text>&lt;html&gt;&lt;body&gt;&lt;img src="HCDISLogo.png" width="30" height="30" /&gt;&lt;/body&gt;&lt;/html&gt;</xsl:text>
                  </w:t>
                </w:r>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="1195" w:type="pct" />
                <w:vMerge w:val="restart" />
                <w:shd w:val="clear" w:color="auto" w:fill="DBE5F1" w:themeFill="accent1" w:themeFillTint="33" />
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00E83BF1" w:rsidR="00131405" w:rsidP="00724DBF" w:rsidRDefault="00131405">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:jc w:val="center" />
                  <w:rPr>
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                    <w:sz w:val="16" />
                    <w:szCs w:val="16" />
                  </w:rPr>
                </w:pPr>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="1012" w:type="pct" />
                <w:shd w:val="clear" w:color="auto" w:fill="DBE5F1" w:themeFill="accent1" w:themeFillTint="33" />
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00E83BF1" w:rsidR="00131405" w:rsidP="00724DBF" w:rsidRDefault="00131405">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:jc w:val="right" />
                  <w:rPr>
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                </w:pPr>
                <w:r w:rsidRPr="00E83BF1">
                  <w:rPr>
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                  <w:t>Doctor</w:t>
                </w:r>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="2044" w:type="pct" />
                <w:tcBorders>
                  <w:top w:val="single" w:color="DBE5F1" w:themeColor="accent1" w:themeTint="33" w:sz="12" w:space="0" />
                  <w:bottom w:val="nil" />
                </w:tcBorders>
                <w:shd w:val="clear" w:color="auto" w:fill="FFFFFF" w:themeFill="background1" />
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00E83BF1" w:rsidR="00131405" w:rsidP="00724DBF" w:rsidRDefault="00131405">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:rPr>
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                </w:pPr>
                <w:r w:rsidRPr="00E83BF1">
                  <w:rPr>
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
          <w:tr w:rsidR="00131405" w:rsidTr="00724DBF">
            <w:trPr>
              <w:trHeight w:val="226" />
            </w:trPr>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="748" w:type="pct" />
                <w:vMerge />
                <w:shd w:val="clear" w:color="auto" w:fill="DBE5F1" w:themeFill="accent1" w:themeFillTint="33" />
              </w:tcPr>
              <w:p w:rsidRPr="00E83BF1" w:rsidR="00131405" w:rsidP="00724DBF" w:rsidRDefault="00131405">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:rPr>
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                  </w:rPr>
                </w:pPr>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="1195" w:type="pct" />
                <w:vMerge />
                <w:shd w:val="clear" w:color="auto" w:fill="DBE5F1" w:themeFill="accent1" w:themeFillTint="33" />
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00E83BF1" w:rsidR="00131405" w:rsidP="00724DBF" w:rsidRDefault="00131405">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:rPr>
                    <w:caps />
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                  </w:rPr>
                </w:pPr>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="1012" w:type="pct" />
                <w:shd w:val="clear" w:color="auto" w:fill="DBE5F1" w:themeFill="accent1" w:themeFillTint="33" />
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00E83BF1" w:rsidR="00131405" w:rsidP="00724DBF" w:rsidRDefault="00131405">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:jc w:val="right" />
                  <w:rPr>
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                </w:pPr>
                <w:r w:rsidRPr="00E83BF1">
                  <w:rPr>
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                  <w:t xml:space="preserve">Nº </w:t>
                </w:r>
                <w:r>
                  <w:rPr>
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                  <w:t>colegiado</w:t>
                </w:r>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="2044" w:type="pct" />
                <w:tcBorders>
                  <w:top w:val="nil" />
                  <w:bottom w:val="nil" />
                </w:tcBorders>
                <w:shd w:val="clear" w:color="auto" w:fill="FFFFFF" w:themeFill="background1" />
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00E83BF1" w:rsidR="00131405" w:rsidP="00724DBF" w:rsidRDefault="00131405">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:rPr>
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                </w:pPr>
                <w:r w:rsidRPr="00E83BF1">
                  <w:rPr>
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
          <w:tr w:rsidR="00131405" w:rsidTr="00724DBF">
            <w:trPr>
              <w:trHeight w:val="226" />
            </w:trPr>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="748" w:type="pct" />
                <w:vMerge />
                <w:shd w:val="clear" w:color="auto" w:fill="DBE5F1" w:themeFill="accent1" w:themeFillTint="33" />
              </w:tcPr>
              <w:p w:rsidRPr="00E83BF1" w:rsidR="00131405" w:rsidP="00724DBF" w:rsidRDefault="00131405">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:rPr>
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                  </w:rPr>
                </w:pPr>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="1195" w:type="pct" />
                <w:vMerge />
                <w:shd w:val="clear" w:color="auto" w:fill="DBE5F1" w:themeFill="accent1" w:themeFillTint="33" />
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00E83BF1" w:rsidR="00131405" w:rsidP="00724DBF" w:rsidRDefault="00131405">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:rPr>
                    <w:caps />
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                  </w:rPr>
                </w:pPr>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="1012" w:type="pct" />
                <w:shd w:val="clear" w:color="auto" w:fill="DBE5F1" w:themeFill="accent1" w:themeFillTint="33" />
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00E83BF1" w:rsidR="00131405" w:rsidP="00724DBF" w:rsidRDefault="00131405">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:jc w:val="right" />
                  <w:rPr>
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                </w:pPr>
                <w:r>
                  <w:rPr>
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                  <w:t>Servicio</w:t>
                </w:r>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="2044" w:type="pct" />
                <w:tcBorders>
                  <w:top w:val="nil" />
                  <w:bottom w:val="single" w:color="DBE5F1" w:themeColor="accent1" w:themeTint="33" w:sz="12" w:space="0" />
                </w:tcBorders>
                <w:shd w:val="clear" w:color="auto" w:fill="FFFFFF" w:themeFill="background1" />
                <w:vAlign w:val="center" />
              </w:tcPr>
              <w:p w:rsidRPr="00E83BF1" w:rsidR="00131405" w:rsidP="00724DBF" w:rsidRDefault="00131405">
                <w:pPr>
                  <w:pStyle w:val="Encabezado" />
                  <w:rPr>
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                </w:pPr>
                <w:r w:rsidRPr="00E83BF1">
                  <w:rPr>
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

        <!--<w:p w:rsidR="00131405" w:rsidRDefault="00131405">
          <w:pPr>
            <w:pStyle w:val="Piedepgina" />
          </w:pPr>
        </w:p>
        <w:p w:rsidR="00131405" w:rsidRDefault="00131405">
          <w:pPr>
            <w:pStyle w:val="Piedepgina" />
          </w:pPr>
        </w:p>-->
      </w:ftr>
      <!-- Fin pie página -->
    </wordDoc>
  </xsl:template>

  <xsl:template match="ReportTemplates/ReportTemplateDTO" xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main">
    <w:tbl xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main" >
      <w:tblPr>
        <w:tblStyle w:val="Tablaconcuadrcula" />
        <w:tblW w:w="0" w:type="auto" />
        <w:tblBorders>
          <w:top w:val="none" w:color="auto" w:sz="0" w:space="0" />
          <w:left w:val="none" w:color="auto" w:sz="0" w:space="0" />
          <w:bottom w:val="none" w:color="auto" w:sz="0" w:space="0" />
          <w:right w:val="none" w:color="auto" w:sz="0" w:space="0" />
          <w:insideH w:val="none" w:color="auto" w:sz="0" w:space="0" />
          <w:insideV w:val="none" w:color="auto" w:sz="0" w:space="0" />
        </w:tblBorders>
        <w:shd w:val="clear" w:color="auto" w:fill="DBE5F1" w:themeFill="accent1" w:themeFillTint="33" />
        <w:tblLook w:val="04A0" />
      </w:tblPr>
      <w:tblGrid>
        <w:gridCol w:w="392" />
        <w:gridCol w:w="5670" />
        <w:gridCol w:w="1134" />
        <w:gridCol w:w="1519" />
      </w:tblGrid>
      <w:tr w:rsidRPr="004F2AD5" w:rsidR="00131405" w:rsidTr="00724DBF">
        <w:tc>
          <w:tcPr>
            <w:tcW w:w="6062" w:type="dxa" />
            <w:gridSpan w:val="2" />
            <w:shd w:val="clear" w:color="auto" w:fill="DBE5F1" w:themeFill="accent1" w:themeFillTint="33" />
          </w:tcPr>

          <w:p w:rsidRPr="004F2AD5" w:rsidR="00131405" w:rsidP="00724DBF" w:rsidRDefault="00131405">
            <w:pPr>
              <w:rPr>
                <w:b />
                <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
              </w:rPr>
            </w:pPr>
            <w:r>
              <w:rPr>
                <w:b />
                <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
              </w:rPr>
              <w:t>
                <xsl:value-of select="Title" />
              </w:t>
            </w:r>
          </w:p>
        </w:tc>
        <w:tc>
          <w:tcPr>
            <w:tcW w:w="1134" w:type="dxa" />
            <w:shd w:val="clear" w:color="auto" w:fill="DBE5F1" w:themeFill="accent1" w:themeFillTint="33" />
          </w:tcPr>
          <w:p w:rsidRPr="004F2AD5" w:rsidR="00131405" w:rsidP="00724DBF" w:rsidRDefault="00131405">
            <w:pPr>
              <w:jc w:val="right" />
              <w:rPr>
                <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                <w:sz w:val="18" />
                <w:szCs w:val="18" />
              </w:rPr>
            </w:pPr>
            <w:r w:rsidRPr="004F2AD5">
              <w:rPr>
                <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                <w:sz w:val="18" />
                <w:szCs w:val="18" />
              </w:rPr>
              <w:t>Fecha</w:t>
            </w:r>
          </w:p>
        </w:tc>
        <w:tc>
          <w:tcPr>
            <w:tcW w:w="1519" w:type="dxa" />
            <w:shd w:val="clear" w:color="auto" w:fill="DBE5F1" w:themeFill="accent1" w:themeFillTint="33" />
          </w:tcPr>
          <w:p w:rsidRPr="004F2AD5" w:rsidR="00131405" w:rsidP="00724DBF" w:rsidRDefault="00131405">
            <w:pPr>
              <w:rPr>
                <w:b />
                <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                <w:sz w:val="18" />
                <w:szCs w:val="18" />
              </w:rPr>
            </w:pPr>
            <w:r>
              <w:rPr>
                <w:b />
                <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                <w:sz w:val="18" />
                <w:szCs w:val="18" />
              </w:rPr>
              <w:t>
                <xsl:value-of select="DateTime" />
              </w:t>
            </w:r>
          </w:p>
        </w:tc>
      </w:tr>
      <xsl:apply-templates select="Blocks"/>
    </w:tbl>

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

  <xsl:template match="Blocks/ReportBlockDTO">
    <!--<w:tc xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main">-->
    <w:tr w:rsidRPr="004F2AD5" w:rsidR="00131405" w:rsidTr="00724DBF" xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main">
      <w:tc>
        <w:tcPr>
          <w:tcW w:w="392" w:type="dxa" />
          <w:shd w:val="clear" w:color="auto" w:fill="FFFFFF" w:themeFill="background1" />
        </w:tcPr>
        <w:p w:rsidR="00131405" w:rsidP="00131405" w:rsidRDefault="00131405">
          <w:pPr>
            <w:pStyle w:val="Prrafodelista" />
            <w:numPr>
              <w:ilvl w:val="0" />
              <w:numId w:val="1" />
            </w:numPr>
          </w:pPr>
        </w:p>
      </w:tc>
      <w:tc>
        <w:tcPr>
          <w:tcW w:w="8323" w:type="dxa" />
          <w:gridSpan w:val="3" />
          <w:shd w:val="clear" w:color="auto" w:fill="FFFFFF" w:themeFill="background1" />
        </w:tcPr>
        <w:tbl>
          <w:tblPr>
            <w:tblStyle w:val="Tablaconcuadrcula" />
            <w:tblW w:w="0" w:type="auto" />
            <w:tblBorders>
              <w:top w:val="none" w:color="auto" w:sz="0" w:space="0" />
              <w:left w:val="none" w:color="auto" w:sz="0" w:space="0" />
              <w:bottom w:val="none" w:color="auto" w:sz="0" w:space="0" />
              <w:right w:val="none" w:color="auto" w:sz="0" w:space="0" />
              <w:insideH w:val="none" w:color="auto" w:sz="0" w:space="0" />
              <w:insideV w:val="none" w:color="auto" w:sz="0" w:space="0" />
            </w:tblBorders>
            <w:shd w:val="clear" w:color="auto" w:fill="DBE5F1" w:themeFill="accent1" w:themeFillTint="33" />
            <w:tblLook w:val="04A0" />
          </w:tblPr>
          <w:tblGrid>
            <w:gridCol w:w="222" />
            <w:gridCol w:w="1169" />
            <w:gridCol w:w="1193" />
            <w:gridCol w:w="1169" />
            <w:gridCol w:w="1193" />
          </w:tblGrid>
          <w:tr w:rsidRPr="004F2AD5" w:rsidR="00131405" w:rsidTr="00CE4A54">
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="0" w:type="auto" />
                <w:gridSpan w:val="5" />
                <w:shd w:val="clear" w:color="auto" w:fill="DBE5F1" w:themeFill="accent1" w:themeFillTint="33" />
              </w:tcPr>
              <w:p w:rsidRPr="004F2AD5" w:rsidR="00131405" w:rsidP="00724DBF" w:rsidRDefault="00131405">
                <w:pPr>
                  <w:rPr>
                    <w:b />
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                    <w:sz w:val="18" />
                    <w:szCs w:val="18" />
                  </w:rPr>
                </w:pPr>
                <w:r>
                  <w:rPr>
                    <w:b />
                    <w:color w:val="365F91" w:themeColor="accent1" w:themeShade="BF" />
                  </w:rPr>
                  <w:t>
                    <xsl:value-of select="Title"/>
                  </w:t>
                </w:r>
              </w:p>
            </w:tc>
          </w:tr>
          <xsl:apply-templates select="BlockRows"/>
        </w:tbl>
        <w:p w:rsidR="00131405" w:rsidP="00724DBF" w:rsidRDefault="00131405" />
      </w:tc>
    </w:tr>
    <!--<w:p w:rsidRPr="004F2AD5" w:rsidR="00131405" w:rsidP="00724DBF" w:rsidRDefault="00131405"/>-->
    <!--</w:tc>-->
  </xsl:template>

  <xsl:template match="BlockRows/ReportBlockRowDTO" xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main">
    <w:tr w:rsidR="00131405" w:rsidTr="00CE4A54" xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main">
      <w:trPr>
        <w:trHeight w:val="90" />
      </w:trPr>
      <w:tc>
        <w:tcPr>
          <w:tcW w:w="0" w:type="auto" />
          <w:vMerge w:val="restart" />
          <w:shd w:val="clear" w:color="auto" w:fill="FFFFFF" w:themeFill="background1" />
        </w:tcPr>
        <w:p w:rsidRPr="003021AD" w:rsidR="00131405" w:rsidP="00131405" w:rsidRDefault="00131405">
          <w:pPr>
            <w:pStyle w:val="Prrafodelista" />
            <w:numPr>
              <w:ilvl w:val="0" />
              <w:numId w:val="1" />
            </w:numPr>
            <w:rPr>
              <w:sz w:val="16" />
              <w:szCs w:val="16" />
            </w:rPr>
          </w:pPr>
        </w:p>
      </w:tc>
      <xsl:apply-templates select="Observations"/>
    </w:tr>
  </xsl:template>

  <xsl:template match="Observations/ReportObservationDTO" xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main">
    <xsl:choose>
      <xsl:when test="Type='Label'">
        <w:tc xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main">
          <w:tcPr>
            <w:tcW w:w="0" w:type="auto" />
            <xsl:if test="ColSpan>'1'">
              <w:gridSpan>
                <xsl:attribute name="w:val">
                  <xsl:value-of select="ColSpan"/>
                </xsl:attribute>
              </w:gridSpan>
            </xsl:if>
            <w:shd w:val="clear" w:color="auto" w:fill="EEECE1" w:themeFill="background2" />
          </w:tcPr>
          <w:p w:rsidRPr="003021AD" w:rsidR="00131405" w:rsidP="00724DBF" w:rsidRDefault="00131405">
            <w:pPr>
              <w:rPr>
                <w:color w:val="404040" w:themeColor="text1" w:themeTint="BF" />
                <w:sz w:val="16" />
                <w:szCs w:val="16" />
              </w:rPr>
            </w:pPr>
            <w:r w:rsidRPr="003021AD">
              <w:rPr>
                <w:color w:val="404040" w:themeColor="text1" w:themeTint="BF" />
                <w:sz w:val="16" />
                <w:szCs w:val="16" />
              </w:rPr>
              <w:t>
                <xsl:value-of select="Value"/>
              </w:t>
            </w:r>
          </w:p>
        </w:tc>
      </xsl:when>
      <xsl:otherwise>
        <w:tc xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main">
          <w:tcPr>
            <w:tcW w:w="0" w:type="auto" />
            <xsl:if test="ColSpan>'1'">
              <w:gridSpan>
                <xsl:attribute name="w:val">
                  <xsl:value-of select="ColSpan"/>
                </xsl:attribute>
              </w:gridSpan>
            </xsl:if>
            <w:shd w:val="clear" w:color="auto" w:fill="FFFFFF" w:themeFill="background1" />
          </w:tcPr>
          <w:p w:rsidRPr="003021AD" w:rsidR="00131405" w:rsidP="00724DBF" w:rsidRDefault="00131405">
            <w:pPr>
              <w:rPr>
                <w:sz w:val="16" />
                <w:szCs w:val="16" />
              </w:rPr>
            </w:pPr>
            <w:r w:rsidRPr="003021AD">
              <w:rPr>
                <w:sz w:val="16" />
                <w:szCs w:val="16" />
              </w:rPr>
              <w:t>
                <xsl:choose>
                  <xsl:when test="ValueType='RichText' and Value!=''">
                    <xsl:text>&lt;html&gt;&lt;body&gt;</xsl:text>
                    <xsl:value-of select="Value"/>
                    <xsl:text>&lt;/body&gt;&lt;/html&gt;</xsl:text>
                  </xsl:when>
                  <xsl:when test="ValueType='Boolean'">
                    <xsl:if test="Value='True'">
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
              </w:t>
            </w:r>
          </w:p>
        </w:tc>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

</xsl:stylesheet>
