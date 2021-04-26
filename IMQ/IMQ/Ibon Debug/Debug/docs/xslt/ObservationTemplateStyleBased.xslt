<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <xsl:output method="xml" indent="yes"/>

  <xsl:template match="/">
    <xsl:apply-templates select="CustomerReportTemplateDTO"/>
  </xsl:template>

  <xsl:template match="CustomerReportTemplateDTO">
    <wordDoc>
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
            <w:tblStyle w:val="HCDISDOCHEADER" />
            <w:tblW w:w="5000" w:type="pct" />
            <w:tblLook w:val="04A0" />
          </w:tblPr>
          <w:tblGrid>
            <w:gridCol w:w="2161" />
            <w:gridCol w:w="2161" />
            <w:gridCol w:w="2161" />
            <w:gridCol w:w="2161" />
          </w:tblGrid>
          <w:tr w:rsidR="00737662" w:rsidTr="00E82CD5">
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="750" w:type="pct" xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main" />
              </w:tcPr>
              <w:p w:rsidR="007D4055" w:rsidP="00C85FC0" w:rsidRDefault="00737662">
                <xsl:if test="Options/ShowHeaderLogoLeft='true'">
                  <w:pPr>
                    <w:pStyle w:val="Encabezado" />
                  </w:pPr>
                  <w:r>
                    <w:rPr xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main">
                      <w:lang w:val="en-US" />
                    </w:rPr>
                    <w:t>
                      <xsl:text>&lt;html&gt;&lt;body&gt;&lt;img src="HeaderLogoLeft.jpg" width="100" /&gt;&lt;/body&gt;&lt;/html&gt;</xsl:text>
                    </w:t>
                  </w:r>
                  <!--<xsl:if test="OrganizationName!=''">
                    <w:p w:rsidRPr="00FF5524" w:rsidR="0056541D" w:rsidP="00FF5524" w:rsidRDefault="00FF5524">
                      <w:pPr>
                        <w:pStyle w:val="Citadestacada" />
                        <w:rPr>
                          <w:lang w:val="en-US" />
                        </w:rPr>
                      </w:pPr>
                      <w:r w:rsidRPr="00FF5524">
                        <w:rPr>
                          <w:lang w:val="en-US" />
                        </w:rPr>
                        <w:t>
                          <xsl:value-of select="OrganizationName" />
                        </w:t>
                      </w:r>
                    </w:p>
                  </xsl:if>
                  <xsl:if test="OrganizationAddressLine1!=''">
                    <w:p w:rsidRPr="00FF5524" w:rsidR="00FF5524" w:rsidP="00FF5524" w:rsidRDefault="00FF5524">
                      <w:pPr>
                        <w:pStyle w:val="Citadestacada" />
                        <w:rPr>
                          <w:lang w:val="en-US" />
                        </w:rPr>
                      </w:pPr>
                      <w:r w:rsidRPr="00FF5524">
                        <w:rPr>
                          <w:lang w:val="en-US" />
                        </w:rPr>
                        <w:t>
                          <xsl:value-of select="OrganizationAddressLine1" />
                        </w:t>
                      </w:r>
                    </w:p>
                  </xsl:if>
                  <xsl:if test="OrganizationAddressLine2!=''">
                    <w:p w:rsidRPr="00FF5524" w:rsidR="00FF5524" w:rsidP="00FF5524" w:rsidRDefault="00FF5524">
                      <w:pPr>
                        <w:pStyle w:val="Citadestacada" />
                        <w:rPr>
                          <w:lang w:val="en-US" />
                        </w:rPr>
                      </w:pPr>
                      <w:r w:rsidRPr="00FF5524">
                        <w:rPr>
                          <w:lang w:val="en-US" />
                        </w:rPr>
                        <w:t>
                          <xsl:value-of select="OrganizationAddressLine2" />
                        </w:t>
                      </w:r>
                    </w:p>
                  </xsl:if>
                  <xsl:if test="OrganizationTelephone!=''">
                    <w:p w:rsidRPr="00FF5524" w:rsidR="00FF5524" w:rsidP="00FF5524" w:rsidRDefault="00FF5524">
                      <w:pPr>
                        <w:pStyle w:val="Citadestacada" />
                        <w:rPr>
                          <w:lang w:val="en-US" />
                        </w:rPr>
                      </w:pPr>
                      <w:r w:rsidRPr="00FF5524">
                        <w:rPr>
                          <w:lang w:val="en-US" />
                        </w:rPr>
                        <w:t>
                          <xsl:value-of select="OrganizationTelephone" />
                        </w:t>
                      </w:r>
                    </w:p>
                  </xsl:if>
                  <xsl:if test="OrganizationEmail!=''">
                    <w:p w:rsidRPr="00FF5524" w:rsidR="00FF5524" w:rsidP="00FF5524" w:rsidRDefault="00FF5524">
                      <w:pPr>
                        <w:pStyle w:val="Citadestacada" />
                        <w:rPr>
                          <w:lang w:val="en-US" />
                        </w:rPr>
                      </w:pPr>
                      <w:r w:rsidRPr="00FF5524">
                        <w:rPr>
                          <w:lang w:val="en-US" />
                        </w:rPr>
                        <w:t>
                          <xsl:value-of select="OrganizationEmail" />
                        </w:t>
                      </w:r>
                    </w:p>
                  </xsl:if>-->
                </xsl:if>
              </w:p>

              <xsl:if test="Options/ShowHeaderLogoLeft='true'">
                <w:p w:rsidR="007D4055" w:rsidP="00C85FC0" w:rsidRDefault="00737662">
                  <w:pPr>
                    <w:pStyle w:val="Encabezado" />
                  </w:pPr>
                  <!--<w:r>
                    <w:rPr xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main">
                      <w:lang w:val="en-US" />
                    </w:rPr>
                    <w:t>
                      <xsl:text>&lt;html&gt;&lt;body&gt;&lt;img src="HeaderLogoLeft.jpg" width="100" /&gt;&lt;/body&gt;&lt;/html&gt;</xsl:text>
                    </w:t>
                  </w:r>-->
                  <xsl:if test="OrganizationName!=''">
                    <w:p w:rsidRPr="00FF5524" w:rsidR="0056541D" w:rsidP="00FF5524" w:rsidRDefault="00FF5524">
                      <w:pPr>
                        <w:pStyle w:val="Citadestacada" />
                        <w:rPr>
                          <w:lang w:val="en-US" />
                        </w:rPr>
                      </w:pPr>
                      <w:r w:rsidRPr="00FF5524">
                        <w:rPr>
                          <w:lang w:val="en-US" />
                        </w:rPr>
                        <w:t>
                          <xsl:value-of select="OrganizationName" />
                        </w:t>
                      </w:r>
                    </w:p>
                  </xsl:if>
                  <xsl:if test="OrganizationAddressLine1!=''">
                    <w:p w:rsidRPr="00FF5524" w:rsidR="00FF5524" w:rsidP="00FF5524" w:rsidRDefault="00FF5524">
                      <w:pPr>
                        <w:pStyle w:val="Citadestacada" />
                        <w:rPr>
                          <w:lang w:val="en-US" />
                        </w:rPr>
                      </w:pPr>
                      <w:r w:rsidRPr="00FF5524">
                        <w:rPr>
                          <w:lang w:val="en-US" />
                        </w:rPr>
                        <w:t>
                          <xsl:value-of select="OrganizationAddressLine1" />
                        </w:t>
                      </w:r>
                    </w:p>
                  </xsl:if>
                  <xsl:if test="OrganizationAddressLine2!=''">
                    <w:p w:rsidRPr="00FF5524" w:rsidR="00FF5524" w:rsidP="00FF5524" w:rsidRDefault="00FF5524">
                      <w:pPr>
                        <w:pStyle w:val="Citadestacada" />
                        <w:rPr>
                          <w:lang w:val="en-US" />
                        </w:rPr>
                      </w:pPr>
                      <w:r w:rsidRPr="00FF5524">
                        <w:rPr>
                          <w:lang w:val="en-US" />
                        </w:rPr>
                        <w:t>
                          <xsl:value-of select="OrganizationAddressLine2" />
                        </w:t>
                      </w:r>
                    </w:p>
                  </xsl:if>
                  <xsl:if test="OrganizationTelephone!=''">
                    <w:p w:rsidRPr="00FF5524" w:rsidR="00FF5524" w:rsidP="00FF5524" w:rsidRDefault="00FF5524">
                      <w:pPr>
                        <w:pStyle w:val="Citadestacada" />
                        <w:rPr>
                          <w:lang w:val="en-US" />
                        </w:rPr>
                      </w:pPr>
                      <w:r w:rsidRPr="00FF5524">
                        <w:rPr>
                          <w:lang w:val="en-US" />
                        </w:rPr>
                        <w:t>
                          <xsl:value-of select="OrganizationTelephone" />
                        </w:t>
                      </w:r>
                    </w:p>
                  </xsl:if>
                  <xsl:if test="OrganizationEmail!=''">
                    <w:p w:rsidRPr="00FF5524" w:rsidR="00FF5524" w:rsidP="00FF5524" w:rsidRDefault="00FF5524">
                      <w:pPr>
                        <w:pStyle w:val="Citadestacada" />
                        <w:rPr>
                          <w:lang w:val="en-US" />
                        </w:rPr>
                      </w:pPr>
                      <w:r w:rsidRPr="00FF5524">
                        <w:rPr>
                          <w:lang w:val="en-US" />
                        </w:rPr>
                        <w:t>
                          <xsl:value-of select="OrganizationEmail" />
                        </w:t>
                      </w:r>
                    </w:p>
                  </xsl:if>
                </w:p>
              </xsl:if>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="3500" w:type="pct" xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main" />
              </w:tcPr>
              <xsl:if test="ReportTitle!=''">
                <w:p w:rsidR="007D4055" w:rsidP="00C85FC0" w:rsidRDefault="00737662">
                  <w:pPr>
                    <w:pStyle w:val="Subttulo" />
                  </w:pPr>
                  <w:r>
                    <w:t>
                      <xsl:value-of select="ReportTitle" />
                    </w:t>
                  </w:r>
                </w:p>
              </xsl:if>
              <xsl:if test="ReportSubtitle!=''">
                <w:p w:rsidRPr="002E7AC8" w:rsidR="002E7AC8" w:rsidP="002E7AC8" w:rsidRDefault="002E7AC8">
                  <w:pPr>
                    <w:pStyle w:val="Cita" />
                    <!--<w:rPr>
                      <w:rStyle w:val="nfasissutil" />
                    </w:rPr>-->
                  </w:pPr>
                  <w:r w:rsidRPr="002E7AC8">
                    <w:rPr>
                      <w:rStyle w:val="nfasissutil" />
                    </w:rPr>
                    <w:t>
                      <xsl:value-of select="ReportSubtitle" />
                    </w:t>
                  </w:r>
                </w:p>
              </xsl:if>
              <xsl:if test="ReportDateTime!=''">
                <w:p w:rsidRPr="006B1988" w:rsidR="00737662" w:rsidP="00E82CD5" w:rsidRDefault="00737662">
                  <w:pPr>
                    <w:pStyle w:val="Cita" />
                    <!--<w:rPr>
                      <w:rStyle w:val="nfasissutil" />
                    </w:rPr>-->
                  </w:pPr>
                  <w:r w:rsidRPr="006B1988">
                    <w:rPr>
                      <w:rStyle w:val="nfasissutil" />
                    </w:rPr>
                    <w:t xml:space="preserve">Fecha de informe: </w:t>
                  </w:r>
                  <w:r w:rsidRPr="006B1988">
                    <w:rPr>
                      <w:rStyle w:val="nfasissutil" />
                    </w:rPr>
                    <w:t>
                      <xsl:value-of select="ReportDateTime" />
                    </w:t>
                  </w:r>
                </w:p>
              </xsl:if>
              <w:p w:rsidR="007D4055" w:rsidP="00C85FC0" w:rsidRDefault="00737662"/>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="375" w:type="pct" xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main" />
              </w:tcPr>
              <w:p w:rsidR="007D4055" w:rsidP="00C85FC0" w:rsidRDefault="00737662">
                <xsl:if test="Options/ShowHeaderLogoRight1='true'">
                  <w:pPr>
                    <w:pStyle w:val="Encabezado" />
                  </w:pPr>
                  <w:r>
                    <w:rPr xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main">
                      <w:lang w:val="en-US" />
                    </w:rPr>
                    <w:t>
                      <xsl:text>&lt;html&gt;&lt;body&gt;&lt;img src="HeaderLogoRight1.jpg" width="100" height = "100" /&gt;&lt;/body&gt;&lt;/html&gt;</xsl:text>
                    </w:t>
                  </w:r>
                </xsl:if>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="375" w:type="pct" xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main" />
              </w:tcPr>
              <w:p w:rsidR="007D4055" w:rsidP="00C85FC0" w:rsidRDefault="00737662">
                <xsl:if test="Options/ShowHeaderLogoRight2='true'">
                  <w:pPr>
                    <w:pStyle w:val="Encabezado" />
                  </w:pPr>
                  <w:r>
                    <w:rPr xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main">
                      <w:lang w:val="en-US" />
                    </w:rPr>
                    <w:t>
                      <xsl:text>&lt;html&gt;&lt;body&gt;&lt;img src="HeaderLogoRight2.jpg" width="100" height = "100" /&gt;&lt;/body&gt;&lt;/html&gt;</xsl:text>
                    </w:t>
                  </w:r>
                </xsl:if>
              </w:p>
            </w:tc>
          </w:tr>
        </w:tbl>
        <w:p w:rsidR="007D4055" w:rsidP="00FF5524" w:rsidRDefault="00FF5524" xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main">
          <w:pPr>
            <w:pStyle w:val="Citadestacada" />
            <w:jc w:val="center" />
          </w:pPr>
          <w:r>
            <w:t>
              <xsl:if test="Options/ShowCopyright='true'">
                <xsl:value-of select="Copyright" />
              </xsl:if>
            </w:t>
          </w:r>
        </w:p>
      </w:hdr>
      <!-- Fin cabecera -->

      <!-- Inicio cuerpo-->
      <w:document xmlns:ve="http://schemas.openxmlformats.org/markup-compatibility/2006"
                  xmlns:o="urn:schemas-microsoft-com:office:office"
                  xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships"
                  xmlns:m="http://schemas.openxmlformats.org/officeDocument/2006/math"
                  xmlns:v="urn:schemas-microsoft-com:vml"
                  xmlns:wp="http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing"
                  xmlns:w10="urn:schemas-microsoft-com:office:word"
                  xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main"
                  xmlns:wne="http://schemas.microsoft.com/office/word/2006/wordml">
        <w:body>
          <xsl:apply-templates select="ReportTemplates"/>

          <!-- Comprobar valor de parametro SignEnabled-->
          <xsl:if test="Options/SignEnabled='true'">
            <w:p w:rsidR="0017157C" w:rsidP="0017157C" w:rsidRDefault="0017157C" />

            <w:tbl xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main">
              <w:tblPr>
                <w:tblStyle w:val="HCDISCUSTOMER" />
                <w:tblW w:w="5000" w:type="pct" />
                <w:tblLook w:val="04A0" />
              </w:tblPr>
              <w:tblGrid>
                <w:gridCol w:w="8720" />
              </w:tblGrid>
              <w:tr w:rsidR="00C101F9" w:rsidTr="00C101F9">
                <w:tc>
                  <w:tcPr>
                    <w:tcW w:w="5000" w:type="pct" />
                  </w:tcPr>
                  <w:p w:rsidR="00C101F9" w:rsidP="00AD0FB6" w:rsidRDefault="00C101F9" />
                </w:tc>
              </w:tr>
              <w:tr w:rsidR="00C101F9" w:rsidTr="00C101F9">
                <w:tc>
                  <w:tcPr>
                    <w:tcW w:w="5000" w:type="pct" />
                  </w:tcPr>
                  <w:p w:rsidR="00C101F9" w:rsidP="00AD0FB6" w:rsidRDefault="00C101F9" />
                </w:tc>
              </w:tr>
              <w:tr w:rsidR="00C101F9" w:rsidTr="00C101F9">
                <w:tc>
                  <w:tcPr>
                    <w:tcW w:w="5000" w:type="pct" />
                  </w:tcPr>
                  <w:p w:rsidR="00C101F9" w:rsidP="00AD0FB6" w:rsidRDefault="00C101F9" />
                </w:tc>
              </w:tr>
              <w:tr w:rsidR="00C101F9" w:rsidTr="00C101F9">
                <w:tc>
                  <w:tcPr>
                    <w:tcW w:w="5000" w:type="pct" />
                  </w:tcPr>
                  <w:p w:rsidR="00C101F9" w:rsidP="00AD0FB6" w:rsidRDefault="00C101F9" />
                </w:tc>
              </w:tr>
              <xsl:if test="AttendingPhysicianName!=''">
                <w:tr w:rsidR="00C101F9" w:rsidTr="00C101F9">
                  <w:tc>
                    <w:tcPr>
                      <w:tcW w:w="5000" w:type="pct" />
                    </w:tcPr>
                    <w:p w:rsidRPr="00CE2667" w:rsidR="00C101F9" w:rsidP="00AD0FB6" w:rsidRDefault="00C101F9">
                      <w:pPr>
                        <w:rPr>
                          <w:rStyle w:val="nfasis" />
                        </w:rPr>
                      </w:pPr>
                      <w:r w:rsidRPr="00CE2667">
                        <w:rPr>
                          <w:rStyle w:val="nfasis" />
                        </w:rPr>
                        <w:t>
                          Fdo: <xsl:value-of select="AttendingPhysicianName" />
                        </w:t>
                      </w:r>
                      <w:r w:rsidRPr="00CE2667">
                        <w:rPr>
                          <w:rStyle w:val="nfasis" />
                        </w:rPr>
                        <!--<w:t>
                          <xsl:value-of select="AttendingPhysicianName" />
                        </w:t>-->
                        <xsl:if test="AttendingCollegiateNumber!=''">
                          <w:t>
                            . Nº de colegiado: <xsl:value-of select="AttendingCollegiateNumber" />
                          </w:t>
                          <!--<w:t>
                            <xsl:value-of select="AttendingCollegiateNumber" />
                          </w:t>-->
                        </xsl:if>
                      </w:r>
                    </w:p>
                  </w:tc>
                </w:tr>
              </xsl:if>
            </w:tbl>
          </xsl:if>
          <w:p w:rsidR="0017157C" w:rsidP="0017157C" w:rsidRDefault="0017157C" />

          <w:sectPr w:rsidR="00C101F9" w:rsidSect="00AB698C" xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main">
            <!--<w:headerReference w:type="even" r:id="rId7" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships" />
            -->
            <!--<w:headerReference w:type="default" r:id="rId8" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships" />-->
            <!--
            <w:footerReference w:type="even" r:id="rId9" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships" />
            -->
            <!--<w:footerReference w:type="default" r:id="rId10" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships" />-->
            <!--
            <w:headerReference w:type="first" r:id="rId11" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships" />
            <w:footerReference w:type="first" r:id="rId12" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships" />-->
            <w:pgSz w:w="11906" w:h="16838" />
            <w:pgMar w:top="1417" w:right="1701" w:bottom="1417" w:left="1701" w:header="567" w:footer="454" w:gutter="0" />
            <w:cols w:space="708" />
            <w:docGrid w:linePitch="360" />
          </w:sectPr>
        </w:body>
      </w:document>
      <!-- Fin cuerpo -->

      <!-- Inicio pie página-->
      <w:ftr xmlns:ve="http://schemas.openxmlformats.org/markup-compatibility/2006" xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships" xmlns:m="http://schemas.openxmlformats.org/officeDocument/2006/math" xmlns:v="urn:schemas-microsoft-com:vml" xmlns:wp="http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing" xmlns:w10="urn:schemas-microsoft-com:office:word" xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main" xmlns:wne="http://schemas.microsoft.com/office/word/2006/wordml">
        <w:tbl>
          <w:tblPr>
            <w:tblStyle w:val="HCDISDOCFOOTER" />
            <w:tblW w:w="5000" w:type="pct" />
            <w:tblLook w:val="04A0" />
          </w:tblPr>
          <w:tblGrid>
            <w:gridCol w:w="3191" />
            <w:gridCol w:w="2337" />
            <w:gridCol w:w="3192" />
          </w:tblGrid>
          <w:tr w:rsidR="007D4055" w:rsidTr="00FC3588">
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="1000" w:type="pct" />
              </w:tcPr>
              <w:p w:rsidR="007D4055" w:rsidRDefault="00BF5B84">
                <w:pPr>
                  <w:pStyle w:val="Piedepgina" />
                </w:pPr>
                <xsl:if test="Options/ShowFooterLogoLeft='true'">
                  <w:r>
                    <w:rPr xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main">
                      <w:lang w:val="en-US" />
                    </w:rPr>
                    <w:t>
                      <xsl:text>&lt;html&gt;&lt;body&gt;&lt;img src="FooterLogoLeft.jpg" width="30" height="30" /&gt;&lt;/body&gt;&lt;/html&gt;</xsl:text>
                    </w:t>
                  </w:r>
                </xsl:if>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="3000" w:type="pct" />
              </w:tcPr>
              <w:p w:rsidRPr="00BF5B84" w:rsidR="007D4055" w:rsidP="00E82CD5" w:rsidRDefault="00BF5B84">
                <w:pPr>
                  <w:pStyle w:val="Cita" />
                  <w:rPr>
                    <w:rStyle w:val="nfasissutil" />
                  </w:rPr>
                </w:pPr>
                <w:r w:rsidRPr="00BF5B84">
                  <w:rPr>
                    <w:rStyle w:val="nfasissutil" />
                  </w:rPr>
                  <w:t xml:space="preserve">Página </w:t>
                </w:r>
                <w:r w:rsidRPr="00BF5B84" w:rsidR="00051DD4">
                  <w:rPr>
                    <w:rStyle w:val="nfasissutil" />
                  </w:rPr>
                  <w:fldChar w:fldCharType="begin" />
                </w:r>
                <w:r w:rsidRPr="00BF5B84">
                  <w:rPr>
                    <w:rStyle w:val="nfasissutil" />
                  </w:rPr>
                  <w:instrText xml:space="preserve"> PAGE   \* MERGEFORMAT </w:instrText>
                </w:r>
                <w:r w:rsidRPr="00BF5B84" w:rsidR="00051DD4">
                  <w:rPr>
                    <w:rStyle w:val="nfasissutil" />
                  </w:rPr>
                  <w:fldChar w:fldCharType="separate" />
                </w:r>
                <w:r w:rsidR="00687F48">
                  <w:rPr>
                    <w:rStyle w:val="nfasissutil" />
                    <w:noProof />
                  </w:rPr>
                  <w:t>1</w:t>
                </w:r>
                <w:r w:rsidRPr="00BF5B84" w:rsidR="00051DD4">
                  <w:rPr>
                    <w:rStyle w:val="nfasissutil" />
                  </w:rPr>
                  <w:fldChar w:fldCharType="end" />
                </w:r>
                <w:r w:rsidRPr="00BF5B84">
                  <w:rPr>
                    <w:rStyle w:val="nfasissutil" />
                  </w:rPr>
                  <w:t xml:space="preserve"> de </w:t>
                </w:r>
                <w:fldSimple w:instr=" SECTIONPAGES   \* MERGEFORMAT ">
                  <w:r w:rsidRPr="00687F48" w:rsidR="00687F48">
                    <w:rPr>
                      <w:rStyle w:val="nfasissutil" />
                      <w:noProof />
                    </w:rPr>
                    <w:t>1</w:t>
                  </w:r>
                </w:fldSimple>
              </w:p>
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="1000" w:type="pct" />
              </w:tcPr>
              <w:p w:rsidR="007D4055" w:rsidRDefault="00BF5B84">
                <xsl:if test="Options/ShowFooterLogoRight='true'">
                  <w:pPr>
                    <w:pStyle w:val="Piedepgina" />
                  </w:pPr>
                  <w:r>
                    <w:rPr xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main">
                      <w:lang w:val="en-US" />
                    </w:rPr>
                    <w:t>
                      <xsl:text>&lt;html&gt;&lt;body&gt;&lt;img src="FooterLogoRight.jpg" width="50" height = "50" /&gt;&lt;/body&gt;&lt;/html&gt;</xsl:text>
                    </w:t>
                  </w:r>
                </xsl:if>
              </w:p>
            </w:tc>
          </w:tr>
        </w:tbl>
      </w:ftr>
      <!-- Fin pie página -->
    </wordDoc>
  </xsl:template>

  <xsl:template match="ReportTemplates/ReportTemplateDTO" xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main">

    <!-- SALTO DE PAGINA CONDICIONAL-->
    <xsl:if test="count(preceding-sibling::*)!=0 and PageBreakBeforeTemplate='true'">
      <w:p w:rsidR="00757170" w:rsidRDefault="00757170" xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main">
        <w:r>
          <w:br w:type="page" />
        </w:r>
      </w:p>
    </xsl:if>

    <!--<w:p w:rsidR="00FC3588" w:rsidP="00FD26BF" w:rsidRDefault="00FC3588">
      <w:r>
        <w:rPr>
          <w:rStyle w:val="nfasisintenso" />
        </w:rPr>
        <w:t>
          <xsl:value-of select="PageBreakAfterTemplate" />
          <xsl:value-of select="../../RepeatHeaderAfterPageBreakTemplate" />
          <xsl:value-of select="preceding-sibling::ReportTemplateDTO[1]/Title" />
          <xsl:value-of select="preceding-sibling::ReportTemplateDTO[1]/PageBreakAfterTemplate" />
        </w:t>
      </w:r>
    </w:p>-->

    <xsl:if test="count(preceding-sibling::*)=0 or (../../RepeatHeaderAfterPageBreakTemplate='true' and preceding-sibling::ReportTemplateDTO[1]/PageBreakAfterTemplate='true')">
      <w:tbl>
        <w:tblPr>
          <w:tblStyle w:val="HCDISCUSTOMER" />
          <w:tblW w:w="5000" w:type="pct" />
          <w:tblLook w:val="0200" />
        </w:tblPr>
        <w:tblGrid>
          <w:gridCol w:w="2161" />
          <w:gridCol w:w="2161" />
          <w:gridCol w:w="2161" />
          <w:gridCol w:w="2161" />
        </w:tblGrid>
        <w:tr w:rsidR="00FC3588" w:rsidTr="00E82CD5">
          <w:tc>
            <w:tcPr>
              <w:cnfStyle w:val="000010000000" />
              <w:tcW w:w="750" w:type="pct" />
            </w:tcPr>
            <w:p w:rsidR="00FC3588" w:rsidP="00FD26BF" w:rsidRDefault="00FC3588">
              <w:r>
                <w:rPr>
                  <w:rStyle w:val="nfasis" />
                </w:rPr>
                <w:t>Paciente:</w:t>
              </w:r>
            </w:p>
          </w:tc>
          <w:tc>
            <w:tcPr>
              <w:cnfStyle w:val="000001000000" />
              <w:tcW w:w="1750" w:type="pct" />
            </w:tcPr>
            <w:p w:rsidR="00FC3588" w:rsidP="00FD26BF" w:rsidRDefault="00FC3588">
              <w:r>
                <w:rPr>
                  <w:rStyle w:val="nfasisintenso" />
                </w:rPr>
                <w:t>
                  <xsl:value-of select="../../FullName" />
                </w:t>
              </w:r>
            </w:p>
          </w:tc>
          <w:tc>
            <w:tcPr>
              <w:cnfStyle w:val="000010000000" />
              <w:tcW w:w="750" w:type="pct" />
            </w:tcPr>
            <w:p w:rsidR="00FC3588" w:rsidP="00FD26BF" w:rsidRDefault="00FC3588">
              <w:r>
                <w:rPr>
                  <w:rStyle w:val="nfasis" />
                </w:rPr>
                <w:t>Nº Historia:</w:t>
              </w:r>
            </w:p>
          </w:tc>
          <w:tc>
            <w:tcPr>
              <w:cnfStyle w:val="000001000000" />
              <w:tcW w:w="1750" w:type="pct" />
            </w:tcPr>
            <w:p w:rsidR="00FC3588" w:rsidP="00FD26BF" w:rsidRDefault="00FC3588">
              <w:r>
                <w:rPr>
                  <w:rStyle w:val="nfasisintenso" />
                </w:rPr>
                <w:t>
                  <xsl:value-of select="../../CustomerCH" />
                </w:t>
              </w:r>
            </w:p>
          </w:tc>
        </w:tr>
        <w:tr w:rsidR="00FC3588" w:rsidTr="00E82CD5">
          <w:tc>
            <w:tcPr>
              <w:cnfStyle w:val="000010000000" />
              <w:tcW w:w="750" w:type="pct" />
            </w:tcPr>
            <w:p w:rsidR="00FC3588" w:rsidP="002E7AC8" w:rsidRDefault="002E7AC8">
              <w:r>
                <w:rPr>
                  <w:rStyle w:val="nfasis" />
                </w:rPr>
                <w:t>Edad:</w:t>
              </w:r>
            </w:p>
          </w:tc>
          <w:tc>
            <w:tcPr>
              <w:cnfStyle w:val="000001000000" />
              <w:tcW w:w="1750" w:type="pct" />
            </w:tcPr>
            <w:p w:rsidR="00FC3588" w:rsidP="00FD26BF" w:rsidRDefault="002E7AC8">
              <w:r>
                <w:rPr>
                  <w:rStyle w:val="nfasisintenso" />
                </w:rPr>
                <w:t>
                  <xsl:value-of select="../../Age" />
                </w:t>
              </w:r>
            </w:p>
          </w:tc>
          <w:tc>
            <w:tcPr>
              <w:cnfStyle w:val="000010000000" />
              <w:tcW w:w="750" w:type="pct" />
            </w:tcPr>
            <w:p w:rsidR="00FC3588" w:rsidP="00FD26BF" w:rsidRDefault="00FC3588">
              <w:r>
                <w:rPr>
                  <w:rStyle w:val="nfasis" />
                </w:rPr>
                <xsl:if test="../../InsurerName!=''">
                  <w:t>Aseguradora:</w:t>
                </xsl:if>
              </w:r>
            </w:p>
          </w:tc>
          <w:tc>
            <w:tcPr>
              <w:cnfStyle w:val="000001000000" />
              <w:tcW w:w="1750" w:type="pct" />
            </w:tcPr>
            <w:p w:rsidR="00FC3588" w:rsidP="00FD26BF" w:rsidRDefault="00FC3588">
              <w:r>
                <w:rPr>
                  <w:rStyle w:val="nfasisintenso" />
                </w:rPr>
                <w:t>
                  <xsl:if test="../../InsurerName!=''">
                    <xsl:value-of select="../../InsurerName" />
                  </xsl:if>
                </w:t>
              </w:r>
            </w:p>
          </w:tc>
        </w:tr>
        <w:tr w:rsidR="00FC3588" w:rsidTr="00E82CD5">
          <w:tc>
            <w:tcPr>
              <w:cnfStyle w:val="000010000000" />
              <w:tcW w:w="750" w:type="pct" />
            </w:tcPr>
            <w:p w:rsidR="00FC3588" w:rsidP="00FD26BF" w:rsidRDefault="00FC3588">
              <w:r>
                <w:rPr>
                  <w:rStyle w:val="nfasis" />
                </w:rPr>
                <w:t>Sexo:</w:t>
              </w:r>
            </w:p>
          </w:tc>
          <w:tc>
            <w:tcPr>
              <w:cnfStyle w:val="000001000000" />
              <w:tcW w:w="1750" w:type="pct" />
            </w:tcPr>
            <w:p w:rsidR="00FC3588" w:rsidP="00FD26BF" w:rsidRDefault="00FC3588">
              <w:r>
                <w:rPr>
                  <w:rStyle w:val="nfasisintenso" />
                </w:rPr>
                <w:t>
                  <xsl:value-of select="../../Sex" />
                </w:t>
              </w:r>
            </w:p>
          </w:tc>
          <w:tc>
            <w:tcPr>
              <w:cnfStyle w:val="000010000000" />
              <w:tcW w:w="750" w:type="pct" />
            </w:tcPr>
            <w:p w:rsidR="00FC3588" w:rsidP="00FD26BF" w:rsidRDefault="00FC3588">
              <w:r>
                <w:rPr>
                  <w:rStyle w:val="nfasis" />
                </w:rPr>
                <xsl:if test="../../PolicyNumber!=''">
                  <w:t>Póliza:</w:t>
                </xsl:if>
              </w:r>
            </w:p>
          </w:tc>
          <w:tc>
            <w:tcPr>
              <w:cnfStyle w:val="000001000000" />
              <w:tcW w:w="1750" w:type="pct" />
            </w:tcPr>
            <w:p w:rsidR="00FC3588" w:rsidP="00FD26BF" w:rsidRDefault="00FC3588">
              <w:r>
                <w:rPr>
                  <w:rStyle w:val="nfasisintenso" />
                </w:rPr>
                <w:t>
                  <xsl:if test="../../PolicyNumber!=''">
                    <xsl:value-of select="../../PolicyNumber" />
                  </xsl:if>
                </w:t>
              </w:r>
            </w:p>
          </w:tc>
        </w:tr>
        <w:tr w:rsidR="00FC3588" w:rsidTr="00E82CD5">
          <w:tc>
            <w:tcPr>
              <w:cnfStyle w:val="000010000000" />
              <w:tcW w:w="750" w:type="pct" />
            </w:tcPr>
            <w:p w:rsidR="00FC3588" w:rsidP="00FD26BF" w:rsidRDefault="002E7AC8">
              <w:r>
                <w:rPr>
                  <w:rStyle w:val="nfasis" />
                </w:rPr>
                <xsl:if test="../../PhysicianName!=''">
                  <w:t>Solicitante:</w:t>
                </xsl:if>
              </w:r>
            </w:p>
          </w:tc>
          <w:tc>
            <w:tcPr>
              <w:cnfStyle w:val="000001000000" />
              <w:tcW w:w="1750" w:type="pct" />
            </w:tcPr>
            <w:p w:rsidR="00FC3588" w:rsidP="00FD26BF" w:rsidRDefault="00FC3588">
              <w:r>
                <w:rPr>
                  <w:rStyle w:val="nfasisintenso" />
                </w:rPr>
                <w:t>
                  <xsl:value-of select="../../PhysicianName" />
                </w:t>
              </w:r>
            </w:p>
          </w:tc>
          <w:tc>
            <w:tcPr>
              <w:cnfStyle w:val="000010000000" />
              <w:tcW w:w="750" w:type="pct" />
            </w:tcPr>
            <w:p w:rsidR="00FC3588" w:rsidP="00FD26BF" w:rsidRDefault="00FC3588">
              <w:r>
                <w:rPr>
                  <w:rStyle w:val="nfasis" />
                </w:rPr>
                <xsl:if test="../../MedEpisodeNumber!=''">
                  <w:t>Nº Episodio:</w:t>
                </xsl:if>
              </w:r>
            </w:p>
          </w:tc>
          <w:tc>
            <w:tcPr>
              <w:cnfStyle w:val="000001000000" />
              <w:tcW w:w="1750" w:type="pct" />
            </w:tcPr>
            <w:p w:rsidR="00FC3588" w:rsidP="00FD26BF" w:rsidRDefault="00FC3588">
              <w:r>
                <w:rPr>
                  <w:rStyle w:val="nfasisintenso" />
                </w:rPr>
                <w:t>
                  <xsl:if test="../../MedEpisodeNumber!=''">
                    <xsl:value-of select="../../MedEpisodeNumber" />
                  </xsl:if>
                </w:t>
              </w:r>
            </w:p>
          </w:tc>
        </w:tr>
      </w:tbl>
    </xsl:if>

    <w:p w:rsidR="0017157C" w:rsidP="0017157C" w:rsidRDefault="0017157C" />

    <w:tbl  xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main">
      <w:tblPr>
        <w:tblStyle w:val="HCDISOBSTEMPLATE" />
        <w:tblW w:w="8514" w:type="dxa" />
        <w:tblInd w:w="-5" w:type="dxa" />
        <w:tblLook w:val="04A0" />
      </w:tblPr>
      <w:tblGrid>
        <w:gridCol w:w="6951" />
        <w:gridCol w:w="1563" />
      </w:tblGrid>
      <w:tr w:rsidR="0017157C" w:rsidTr="0017157C">
        <w:trPr>
          <w:cnfStyle w:val="100000000000" />
        </w:trPr>
        <w:tc>
          <w:tcPr>
            <w:tcW w:w="6951" w:type="dxa" />
          </w:tcPr>
          <w:p w:rsidR="0017157C" w:rsidP="0017157C" w:rsidRDefault="0017157C">
            <w:r>
              <w:rPr>
                <w:rStyle w:val="Ttulodellibro" />
              </w:rPr>
              <w:t>
                <xsl:value-of select="Title" />
              </w:t>
              <xsl:if test="AttendingCollegiateNumber!='' and ../../Options/ShowEpisodePhysician='true'">
                <w:t>
                  . Nº de colegiado: <xsl:value-of select="AttendingCollegiateNumber" />
                </w:t>
                <!--<w:t>
                  <xsl:value-of select="AttendingCollegiateNumber" />
                </w:t>-->
              </xsl:if>
            </w:r>
          </w:p>
        </w:tc>
        <w:tc>
          <w:tcPr>
            <w:tcW w:w="1563" w:type="dxa" />
          </w:tcPr>
          <w:p w:rsidR="0017157C" w:rsidP="0017157C" w:rsidRDefault="0017157C">
            <xsl:if test="../../Options/ShowTemplatesDate='true'">
              <w:r>
                <w:rPr>
                  <w:rStyle w:val="Ttulodellibro" />
                </w:rPr>
                <w:t>
                  <xsl:value-of select="DateTime" />
                </w:t>
              </w:r>
            </xsl:if>
          </w:p>
        </w:tc>
      </w:tr>

      <xsl:apply-templates select="Blocks"/>

    </w:tbl>

    <w:p w:rsidR="0017157C" w:rsidP="0017157C" w:rsidRDefault="0017157C">
      <w:pPr>
        <w:spacing w:after="0" w:line="20" w:lineRule="exact" />
      </w:pPr>
    </w:p>


    <!-- SALTO DE PAGINA CONDICIONAL-->
    <xsl:if test="position()!=last()">
      <xsl:if test="PageBreakAfterTemplate='true'">
        <w:p w:rsidR="00757170" w:rsidRDefault="00757170" xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main">
          <w:r>
            <w:br w:type="page" />
          </w:r>
        </w:p>
        <!--<w:p w:rsidR="00615B53" w:rsidP="00615B53" w:rsidRDefault="00615B53">
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
        </w:sectPr>-->
      </xsl:if>
    </xsl:if>
  </xsl:template>

  <xsl:template match="Blocks/ReportBlockDTO">
    <w:tr w:rsidR="0017157C" w:rsidTr="0017157C" xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main">
      <w:tc>
        <w:tcPr>
          <w:tcW w:w="8225" w:type="dxa" />
          <w:gridSpan w:val="2" />
        </w:tcPr>
        <w:tbl>
          <w:tblPr>
            <w:tblStyle w:val="HCDISBLOCK" />
            <w:tblW w:w="5000" w:type="pct" />
            <w:tblLook w:val="04A0" />
          </w:tblPr>
          <w:tblGrid>
            <w:gridCol w:w="286" />
            <w:gridCol w:w="8231" />
          </w:tblGrid>
          <w:tr w:rsidR="0017157C" w:rsidTr="00FE0C60">
            <w:trPr>
              <w:cnfStyle w:val="100000000000" />
            </w:trPr>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="286" w:type="dxa" />
              </w:tcPr>
              <!--<w:tcPr>
                <w:tcW w:w="0" w:type="auto" />
              </w:tcPr>-->
              <w:p w:rsidR="0017157C" w:rsidP="0017157C" w:rsidRDefault="0017157C">
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
                <w:tcW w:w="0" w:type="auto" />
              </w:tcPr>
              <w:p w:rsidR="0017157C" w:rsidP="0017157C" w:rsidRDefault="0017157C">
                <w:r>
                  <w:rPr>
                    <w:rStyle w:val="Ttulodellibro" />
                  </w:rPr>
                  <w:t>
                    <xsl:value-of select="Title"/>
                  </w:t>
                </w:r>
              </w:p>
            </w:tc>
          </w:tr>
          <w:tr w:rsidR="0017157C" w:rsidTr="00FE0C60">
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="286" w:type="dxa" />
              </w:tcPr>
              <w:p w:rsidR="0017157C" w:rsidP="0017157C" w:rsidRDefault="0017157C"/>
            </w:tc>
            <w:tc>
              <w:tbl>
                <w:tblPr>
                  <w:tblStyle w:val="HCDISOBSERVATIONS" />
                  <w:tblW w:w="5000" w:type="pct" />
                  <w:tblLook w:val="04A0" />
                </w:tblPr>
                <w:tblGrid>
                  <w:gridCol w:w="286" />
                  <w:gridCol w:w="1901" />
                  <w:gridCol w:w="1901" />
                  <w:gridCol w:w="1901" />
                  <w:gridCol w:w="1900" />
                </w:tblGrid>
                <xsl:apply-templates select="BlockRows"/>
              </w:tbl>
              <w:p w:rsidR="0017157C" w:rsidP="0017157C" w:rsidRDefault="0017157C"/>
            </w:tc>
          </w:tr>
          <!--<w:tr w:rsidR="0017157C" w:rsidTr="00FE0C60">
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="174" w:type="pct" />
              </w:tcPr>
              <w:p w:rsidR="0017157C" w:rsidP="0017157C" w:rsidRDefault="0017157C" />
            </w:tc>
            <w:tc>
              <w:tcPr>
                <w:tcW w:w="4826" w:type="pct" />
              </w:tcPr>
              <w:tbl>
                <w:tblPr>
                  <w:tblStyle w:val="HCDISOBSERVATIONS" />
                  <w:tblW w:w="5000" w:type="pct" />
                  <w:tblLook w:val="04A0" />
                </w:tblPr>
                <w:tblGrid>
                  <w:gridCol w:w="7929" />
                </w:tblGrid>
                <w:tr w:rsidR="002E7AC8" w:rsidTr="002E7AC8">
                  <w:tc>
                    <w:tcPr>
                      <w:tcW w:w="5000" w:type="pct" />
                    </w:tcPr>
                    <w:tbl>
                      <w:tblPr>
                        <w:tblStyle w:val="HCDISOBSERVATIONS" />
                        <w:tblW w:w="5000" w:type="pct" />
                        <w:tblLook w:val="04A0" />
                      </w:tblPr>
                      <w:tblGrid>
                        <w:gridCol w:w="2113" />
                        <w:gridCol w:w="5796" />
                      </w:tblGrid>
                      <w:tr w:rsidR="002E7AC8" w:rsidTr="00FA6896">
                        <w:tc>
                          <w:tcPr>
                            <w:tcW w:w="1336" w:type="pct" />
                          </w:tcPr>
                          <w:p w:rsidR="002E7AC8" w:rsidP="00E17DFD" w:rsidRDefault="00687F48">
                            <w:r>
                              <w:t>[[LABEL]]</w:t>
                            </w:r>
                          </w:p>
                        </w:tc>
                        <w:tc>
                          <w:tcPr>
                            <w:tcW w:w="3664" w:type="pct" />
                          </w:tcPr>
                          <w:p w:rsidR="002E7AC8" w:rsidP="00E17DFD" w:rsidRDefault="00687F48">
                            <w:r>
                              <w:t>[[VALUE]]</w:t>
                            </w:r>
                          </w:p>
                        </w:tc>
                      </w:tr>
                      <w:tr w:rsidR="0051749C" w:rsidTr="00FA6896">
                        <w:tc>
                          <w:tcPr>
                            <w:tcW w:w="1336" w:type="pct" />
                          </w:tcPr>
                          <w:p w:rsidR="0051749C" w:rsidP="00E17DFD" w:rsidRDefault="00687F48">
                            <w:r>
                              <w:t>[[LABEL]]</w:t>
                            </w:r>
                          </w:p>
                        </w:tc>
                        <w:tc>
                          <w:tcPr>
                            <w:tcW w:w="3664" w:type="pct" />
                          </w:tcPr>
                          <w:p w:rsidR="0051749C" w:rsidP="00E17DFD" w:rsidRDefault="00687F48">
                            <w:r>
                              <w:t>[[VALUE]]</w:t>
                            </w:r>
                          </w:p>
                        </w:tc>
                      </w:tr>
                    </w:tbl>
                    <w:p w:rsidRPr="00623E4F" w:rsidR="002E7AC8" w:rsidP="0017157C" w:rsidRDefault="002E7AC8">
                      <w:pPr>
                        <w:rPr>
                          <w:rStyle w:val="nfasisintenso" />
                        </w:rPr>
                      </w:pPr>
                    </w:p>
                  </w:tc>
                </w:tr>
              </w:tbl>
              <w:p w:rsidR="0017157C" w:rsidP="0017157C" w:rsidRDefault="0017157C" />
            </w:tc>
          </w:tr>-->
        </w:tbl>

        <w:p w:rsidR="0017157C" w:rsidP="0017157C" w:rsidRDefault="0017157C"/>
      </w:tc>
    </w:tr>
  </xsl:template>

  <xsl:template match="BlockRows/ReportBlockRowDTO" xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main">
    <w:tr w:rsidR="0017157C" w:rsidTr="00FA6896">
      <w:tc>
        <w:tcPr>
          <w:tcW w:w="286" w:type="dxa" />
        </w:tcPr>
        <w:p w:rsidR="0017157C" w:rsidP="0017157C" w:rsidRDefault="0017157C">
          <xsl:if test="Observations/ReportObservationDTO[1]/Type='Label'">
            <w:pPr>
              <w:pStyle w:val="Prrafodelista" />
              <w:numPr>
                <w:ilvl w:val="0" />
                <w:numId w:val="1" />
              </w:numPr>
            </w:pPr>
          </xsl:if>
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
            <!-- Si activo, el fondo de las etiquetas es de color gris -->
            <!--<w:shd w:val="clear" w:color="auto" w:fill="EEECE1" w:themeFill="background2" />-->
          </w:tcPr>
          <w:p w:rsidR="002E7AC8" w:rsidP="00E17DFD" w:rsidRDefault="00687F48">
            <w:pPr>
              <w:rPr>
                <w:rStyle w:val="nfasis" />
              </w:rPr>
            </w:pPr>
            <w:r>
              <w:rPr>
                <w:rStyle w:val="nfasis" />
              </w:rPr>
              <w:t>
                <xsl:value-of select="Value"/>
              </w:t>
            </w:r>
          </w:p>
        </w:tc>
      </xsl:when>
      <xsl:when test="Type='NormalValue'">
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
          </w:tcPr>
          <xsl:if test="/CustomerReportTemplateDTO/Options/ShowNormalValues='true'">
            <w:p w:rsidR="002E7AC8" w:rsidP="00E17DFD" w:rsidRDefault="00687F48">
              <w:r>
                <w:rPr>
                  <w:rStyle w:val="Textoennegrita" />
                </w:rPr>
                <w:t>
                  <xsl:value-of select="Value"/>
                </w:t>
              </w:r>
            </w:p>
          </xsl:if>
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
          </w:tcPr>
          <w:p w:rsidR="002E7AC8" w:rsidP="00E17DFD" w:rsidRDefault="00687F48">
            <w:r>
              <w:rPr>
                <w:rStyle w:val="nfasisintenso" />
              </w:rPr>
              <xsl:choose>
                <xsl:when test="ValueType='RichText'">
                  <xsl:if test="Value!=''">
                    <w:t>
                      <xsl:text>&lt;html&gt;&lt;body&gt;</xsl:text>
                      <xsl:value-of select="Value"/>
                      <xsl:text>&lt;/body&gt;&lt;/html&gt;</xsl:text>
                    </w:t>
                  </xsl:if>
                </xsl:when>
                <xsl:when test="ValueType='Boolean'">
                  <xsl:if test="Value='True'">
                    <w:t>
                      <xsl:text>X</xsl:text>
                    </w:t>
                  </xsl:if>
                </xsl:when>
                <xsl:when test="ValueType='MultiMedia'">
                  <xsl:if test="Value!=''">
                    <w:t>
                      <xsl:text>&lt;html&gt;&lt;body&gt;&lt;img src='</xsl:text>
                      <xsl:value-of select="Value"/>
                      <xsl:text>'/&gt;&lt;/body&gt;&lt;/html&gt;</xsl:text>
                    </w:t>
                  </xsl:if>
                </xsl:when>
                <xsl:otherwise>
                  <xsl:call-template name="stringAnalyzer">
                    <xsl:with-param name="sourceString">
                      <xsl:value-of select="Value"/>
                    </xsl:with-param>
                  </xsl:call-template>
                  <!--<xsl:value-of select="Value"/>-->

                </xsl:otherwise>
              </xsl:choose>
            </w:r>
          </w:p>
        </w:tc>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  
  <xsl:template name="stringAnalyzer" xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main">
    <xsl:param name="sourceString"/>
    <xsl:choose>
      <xsl:when test="string-length(substring-before($sourceString, '&#xA;'))> 1">
        <!--<w:t xml:space="preserve">-->
        <!--<w:p>-->
          <w:t>
            <xsl:value-of select="substring-before($sourceString, '&#xA;')"/>
          </w:t>
        <!--</w:p>-->
        <w:br/>
        <xsl:call-template name="stringAnalyzer">
          <xsl:with-param name="sourceString">
            <xsl:value-of select="substring-after($sourceString, '&#xA;')"/>
          </xsl:with-param>
        </xsl:call-template>
      </xsl:when>
      <xsl:otherwise>
        <!--<w:t xml:space="preserve">-->
          <w:t>
            <xsl:value-of select="$sourceString"/>
          </w:t>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

</xsl:stylesheet>
