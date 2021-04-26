<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    xmlns:ms="urn:schemas-microsoft-com:xslt"
    xmlns:dt="urn:schemas-microsoft-com:datatypes"
    exclude-result-prefixes="msxsl">
  <xsl:output method="html" indent="yes" encoding="utf-8" cdata-section-elements="RawValue"/>

  <xsl:template match="/">
    <xsl:apply-templates select="ClinicalHistorySummaryReportDTO" />
  </xsl:template>

  <xsl:template match="ClinicalHistorySummaryReportDTO">
    <html>
      <head>
        <meta http-equiv="Content-Type" content="text/html; charset=windows-1252"/>
        <meta name="Generator" content="Microsoft Word 14 (filtered)"/>
        <style>
          /* Font Definitions */
          @font-face
          {font-family:Cambria;
          panose-1:2 4 5 3 5 4 6 3 2 4;}
          @font-face
          {font-family:Calibri;
          panose-1:2 15 5 2 2 2 4 3 2 4;}
          @font-face
          {font-family:Tahoma;
          panose-1:2 11 6 4 3 5 4 4 2 4;}
          /* Style Definitions */
          p.MsoNormal, li.MsoNormal, div.MsoNormal
          {margin-top:0cm;
          margin-right:0cm;
          margin-bottom:10.0pt;
          margin-left:0cm;
          line-height:115%;
          font-size:11.0pt;
          font-family:"Calibri","sans-serif";}
          h1
          {mso-style-link:"Título 1 Car";
          margin-top:24.0pt;
          margin-right:0cm;
          margin-bottom:0cm;
          margin-left:0cm;
          margin-bottom:.0001pt;
          line-height:115%;
          page-break-after:avoid;
          font-size:14.0pt;
          font-family:"Cambria","serif";
          color:#365F91;}
          h2
          {mso-style-link:"Título 2 Car";
          margin-top:10.0pt;
          margin-right:0cm;
          margin-bottom:0cm;
          margin-left:0cm;
          margin-bottom:.0001pt;
          line-height:115%;
          page-break-after:avoid;
          font-size:13.0pt;
          font-family:"Cambria","serif";
          color:#4F81BD;}
          p.MsoDocumentMap, li.MsoDocumentMap, div.MsoDocumentMap
          {mso-style-link:"Mapa del documento Car";
          margin:0cm;
          margin-bottom:.0001pt;
          font-size:8.0pt;
          font-family:"Tahoma","sans-serif";}
          span.Ttulo1Car
          {mso-style-name:"Título 1 Car";
          mso-style-link:"Título 1";
          font-family:"Cambria","serif";
          color:#365F91;
          font-weight:bold;}
          span.Ttulo2Car
          {mso-style-name:"Título 2 Car";
          mso-style-link:"Título 2";
          font-family:"Cambria","serif";
          color:#4F81BD;
          font-weight:bold;}
          span.MapadeldocumentoCar
          {mso-style-name:"Mapa del documento Car";
          mso-style-link:"Mapa del documento";
          font-family:"Tahoma","sans-serif";}
          .MsoChpDefault
          {font-family:"Calibri","sans-serif";}
          .MsoPapDefault
          {margin-bottom:10.0pt;
          line-height:115%;}
          @page WordSection1
          {size:595.3pt 841.9pt;
          margin:70.85pt 3.0cm 70.85pt 3.0cm;
          border:solid #C6D9F1 1.0pt;
          padding:24.0pt 24.0pt 24.0pt 24.0pt;}
          div.WordSection1
          {page:WordSection1;}
        </style>
      </head>
      <body lang="ES">
        <div class="WordSection1">
          <h2 align="center" style='text-align:center'>
            <xsl:value-of select="CHSummaryHeader/FacilityReportInfo/CareCenterName"/>
          </h2>
          <h2 align='center' style='text-align:center'>
            <span style='color:windowtext'>
              Consultation Report
            </span>
          </h2>
          <p class='MsoNormal'>&#160;</p>

          <table align='center' border='0' cellspacing='0' cellpadding='0' style='border-collapse:collapse;border:none' >
            <tr>
              <td align="center">
                <table class='MsoTableGrid' border='0' cellspacing='0' cellpadding='0' style='border-collapse:collapse;border:none'>
                  <tr>
                    <td width='272' valign='top' style='width:203.85pt;border:solid #C6D9F1 1.0pt; border-bottom:none;background:#C6D9F1;padding:0cm 5.4pt 0cm 5.4pt'>
                      <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>
                        <b>PATIENT</b>
                      </p>
                    </td>
                    <td width='16' valign='top' style='width:11.8pt;border:none;border-right:solid #C6D9F1 1.0pt;padding:0cm 5.4pt 0cm 5.4pt'>
                      <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>&#160;</p>
                    </td>
                    <td width='289' valign='top' style='width:216.55pt;border-top:solid #C6D9F1 1.0pt;border-left:none;border-bottom:none;border-right:solid #C6D9F1 1.0pt;background:#C6D9F1;padding:0cm 5.4pt 0cm 5.4pt'>
                      <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>
                        <b>CONSULTANT</b>
                      </p>
                    </td>
                  </tr>
                  <tr>
                    <td width='272' valign='top' style='width:203.85pt;border-top:none;border-left:solid #C6D9F1 1.0pt;border-bottom:none;border-right:solid #C6D9F1 1.0pt;padding:0cm 5.4pt 0cm 5.4pt'>
                      <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>
                        <b>
                          <span lang='EN-US' style='font-size:9.0pt'>Name:</span>
                        </b>
                        <span lang='EN-US'>
                          &#160;<xsl:value-of select="CHSummaryHeader/CustomerReportInfo/FirstName"/>&#160;<xsl:value-of select="CHSummaryHeader/CustomerReportInfo/LastName"/>&#160;<xsl:value-of select="CHSummaryHeader/CustomerReportInfo/LastName2"/>
                        </span>
                      </p>
                    </td>
                    <td width='16' valign='top' style='width:11.8pt;border:none;border-right:solid #C6D9F1 1.0pt;padding:0cm 5.4pt 0cm 5.4pt'>
                      <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>
                        <span lang='EN-US'>&#160;</span>
                      </p>
                    </td>
                    <td width='289' valign='top' style='width:216.55pt;border:none;border-right:solid #C6D9F1 1.0pt;padding:0cm 5.4pt 0cm 5.4pt'>
                      <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>
                        <b>
                          <span lang='EN-US' style='font-size:9.0pt'>Name:</span>
                        </b>
                        <span lang='EN-US'>
                          &#160;<xsl:value-of select="CHSummaryHeader/ActorSignerReportInfo/AsActor/FirstName"/>&#160;<xsl:value-of select="CHSummaryHeader/ActorSignerReportInfo/AsActor/LastName"/>&#160;<xsl:value-of select="CHSummaryHeader/ActorSignerReportInfo/AsActor/LastName2"/>
                        </span>
                      </p>
                    </td>
                  </tr>
                  <tr>
                    <td width='272' valign='top' style='width:203.85pt;border-top:none;border-left:solid #C6D9F1 1.0pt;border-bottom:none;border-right:solid #C6D9F1 1.0pt;padding:0cm 5.4pt 0cm 5.4pt'>
                      <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>
                        <b>
                          <span style='font-size:9.0pt'>BirthDate:</span>
                        </b>
                        &#160;
                        <xsl:value-of select="ms:format-date(CHSummaryHeader/CustomerReportInfo/BirthDate,'dd/MM/yyyy')"/>
                      </p>
                    </td>
                    <td width='16' valign='top' style='width:11.8pt;border:none;border-right:solid #C6D9F1 1.0pt;padding:0cm 5.4pt 0cm 5.4pt'>
                      <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>&#160;</p>
                    </td>
                    <td width='289' valign='top' style='width:216.55pt;border:none;border-right:solid #C6D9F1 1.0pt;padding:0cm 5.4pt 0cm 5.4pt'>
                      <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>
                        <b>
                          <span lang='EN-US' style='font-size:9.0pt'>Collegiate number:</span>
                        </b>
                        <span lang='EN-US'>
                          &#160;
                          <xsl:value-of select="CHSummaryHeader/ActorSignerReportInfo/AsActor/CollegiateNumber"/>
                        </span>
                      </p>
                    </td>
                  </tr>
                  <tr>
                    <td width='272' valign='top' style='width:203.85pt;border-top:none;border-left:solid #C6D9F1 1.0pt;border-bottom:none;border-right:solid #C6D9F1 1.0pt;padding:0cm 5.4pt 0cm 5.4pt'>
                      <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>
                        <b>
                          <span style='font-size:9.0pt'>Sex:</span>
                        </b>
                        &#160;
                        <xsl:value-of select="CHSummaryHeader/CustomerReportInfo/Sex"/>
                      </p>
                    </td>
                    <td width='16' valign='top' style='width:11.8pt;border:none;border-right:solid #C6D9F1 1.0pt;padding:0cm 5.4pt 0cm 5.4pt'>
                      <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>&#160;</p>
                    </td>
                    <td width='289' valign='top' style='width:216.55pt;border:none;border-right:solid #C6D9F1 1.0pt;padding:0cm 5.4pt 0cm 5.4pt'>
                      <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>
                        <b>
                          <span style='font-size:9.0pt'>Date:</span>
                        </b>
                        &#160;
                        <xsl:value-of select="ms:format-date(CHSummaryHeader/UpdateDate,'dd/MM/yyyy')"/>
                      </p>
                    </td>
                  </tr>
                  <tr style='height:3.05pt'>
                    <td width='272' valign='top' style='width:203.85pt;border:solid #C6D9F1 1.0pt;border-top:none;padding:0cm 5.4pt 0cm 5.4pt;height:3.05pt'>
                      <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>
                        <b>
                          <span style='font-size:9.0pt'>MRN:</span>
                        </b>
                        &#160;
                        <xsl:value-of select="CHSummaryHeader/CustomerReportInfo/CHNumber"/>
                      </p>
                    </td>
                    <td width='16' valign='top' style='width:11.8pt;border:none;border-right:solid #C6D9F1 1.0pt;padding:0cm 5.4pt 0cm 5.4pt;height:3.05pt'>
                      <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>&#160;</p>
                    </td>
                    <td width='289' valign='top' style='width:216.55pt;border-top:none;border-left:none;border-bottom:solid #C6D9F1 1.0pt;border-right:solid #C6D9F1 1.0pt;padding:0cm 5.4pt 0cm 5.4pt;height:3.05pt'>
                      <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>
                        <b>
                          <span style='font-size:9.0pt'>Specialty:</span>
                        </b>
                        &#160;
                        <xsl:value-of select="CHSummaryHeader/ActorSignerReportInfo/AsActor/MedicalSpecialty"/>
                      </p>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td align="center">
                <p class='MsoNormal'>&#160;</p>
                <xsl:if test="CHSummaryBody/AllergiesVisible='true'">
                  <xsl:apply-templates select="CHSummaryBody/Allergies/AllergyNotes" />
                </xsl:if>
              </td>
            </tr>
            <tr>
              <td align="center">
                <!--<p class="MsoNormal">
                  <span lang="EN-US">&#160;</span>
                </p>-->

                <xsl:if test="CHSummaryBody/ImmunizationsVisible='true'">
                  <xsl:if test="count(CHSummaryBody/Immunizations/ImmunizationNotes/ReportTemplateDTO)>0">
                    <p class='MsoNormal'>&#160;</p>
                    <xsl:apply-templates select="CHSummaryBody/Immunizations/ImmunizationNotes" />
                    <!--<p class="MsoNormal">
                    <span lang="EN-US">&#160;</span>
                  </p>-->
                  </xsl:if>
                </xsl:if>
              </td>
            </tr>
            <tr>
              <td align="center">
                <p class='MsoNormal'>&#160;</p>
                <xsl:if test="CHSummaryBody/AlertsVisible='true'">
                  <xsl:apply-templates select="CHSummaryBody/Alerts/AlertNotes" />
                </xsl:if>
              </td>
            </tr>
            <tr>
              <td align="center">
                <!--<p class="MsoNormal">
                  <span lang="EN-US">&#160;</span>
                </p>-->
                <xsl:if test="CHSummaryBody/RecomendationsVisible='true'">
                  <xsl:if test="count(CHSummaryBody/Recomendations/RecomendationNotes/ReportTemplateDTO)>0">
                    <p class='MsoNormal'>&#160;</p>
                    <xsl:apply-templates select="CHSummaryBody/Recomendations/RecomendationNotes" />
                    <!--<p class="MsoNormal">
                    <span lang="EN-US">&#160;</span>
                  </p>-->
                  </xsl:if>
                </xsl:if>
              </td>
            </tr>
            <tr>
              <td align="center">
                <xsl:if test="CHSummaryBody/ProblemsVisible='true'">
                  <xsl:if test="count(CHSummaryBody/Problems/ProblemNotes/ReportTemplateDTO)>0">
                    <p class='MsoNormal'>&#160;</p>
                    <xsl:apply-templates select="CHSummaryBody/Problems/ProblemNotes" />
                  </xsl:if>
                </xsl:if>
              </td>
            </tr>
            <tr>
              <td align="center">
                <!--<p class="MsoNormal">
                  <span lang="EN-US">&#160;</span>
                </p>-->
                <xsl:if test="CHSummaryBody/ReasonsVisible='true'">
                  <xsl:if test="count(CHSummaryBody/Reasons/CHSReasonReportInfo)>0">
                    <p class='MsoNormal'>&#160;</p>
                    <xsl:apply-templates select="CHSummaryBody/Reasons" />
                  </xsl:if>
                </xsl:if>
              </td>
            </tr>
            <tr>
              <td align="center">
                <xsl:apply-templates select="CHSummaryBody/Visits" />
              </td>
            </tr>
            <tr>
              <td align="center">
                <p class='MsoNormal'>&#160;</p>
                <table class="MsoTableGrid" border="1" cellspacing="0" cellpadding="0" style='border-collapse:collapse;border:none'>
                  <tr>
                    <td width='83' valign='top' style='width:62.1pt;border:solid #C6D9F1 1.0pt;border-right:none;background:#C6D9F1;padding:0cm 5.4pt 0cm 5.4pt'>
                      <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>
                        <b>
                          <span lang='EN-US' style='font-size:9.0pt'>Signed by</span>
                        </b>
                      </p>
                    </td>
                    <td width='493' valign='top' style='width:370.1pt;border:solid #C6D9F1 1.0pt;border-left:none;padding:0cm 5.4pt 0cm 5.4pt'>
                      <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>
                        <span lang='EN-US'>
                          &#160;<xsl:value-of select="CHSummaryHeader/ActorSignerReportInfo/AsSigner/FirstName"/>&#160;<xsl:value-of select="CHSummaryHeader/ActorSignerReportInfo/AsSigner/LastName"/>&#160;<xsl:value-of select="CHSummaryHeader/ActorSignerReportInfo/AsSigner/LastName2"/>.&#160;Collegiate number:&#160;<xsl:value-of select="CHSummaryHeader/ActorSignerReportInfo/AsSigner/CollegiateNumber"/>.
                        </span>
                      </p>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
        </div>
      </body>
    </html>
  </xsl:template>

  <xsl:template match="AllergyNotes">
    <table class="MsoTableGrid" border="1" cellspacing="0" cellpadding="0" style='border-collapse:collapse;border:none'>
      <tr>
        <td width='576' colspan='6' valign='top' style='width:432.2pt;border:solid #C6D9F1 1.0pt;background:#C6D9F1;padding:0cm 5.4pt 0cm 5.4pt'>
          <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>
            <b>ALLERGIES</b>
          </p>
        </td>
      </tr>

      <xsl:choose>
        <xsl:when test="count(ReportTemplateDTO)>0">
          <xsl:apply-templates select='ReportTemplateDTO'/>
        </xsl:when>
        <xsl:otherwise>
          <tr>
            <td width='414' colspan='6' valign='top' style='width:310.2pt;border:solid #C6D9F1 1.0pt;border-top:none;padding:0cm 5.4pt 0cm 5.4pt'>
              <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>
                No allergies
              </p>
            </td>
          </tr>
        </xsl:otherwise>
      </xsl:choose>

      <tr height='0'>
        <td width='26' style='border:none'></td>
        <td width='28' style='border:none'></td>
        <td width='132' style='border:none'></td>
        <td width='227' style='border:none'></td>
        <td width='212' style='border:none'></td>
      </tr>

    </table>
  </xsl:template>

  <xsl:template match="ImmunizationNotes">
    <table class="MsoTableGrid" border="1" cellspacing="0" cellpadding="0" style='border-collapse:collapse;border:none'>
      <tr>
        <td width='576' colspan='6' valign='top' style='width:432.2pt;border:solid #C6D9F1 1.0pt;background:#C6D9F1;padding:0cm 5.4pt 0cm 5.4pt'>
          <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>
            <b>IMMUNIZATIONS</b>
          </p>
        </td>
      </tr>

      <xsl:apply-templates select='ReportTemplateDTO'/>

      <tr height='0'>
        <td width='26' style='border:none'></td>
        <td width='28' style='border:none'></td>
        <td width='132' style='border:none'></td>
        <td width='227' style='border:none'></td>
        <td width='260' style='border:none'></td>
      </tr>
    </table>
  </xsl:template>

  <xsl:template match="AlertNotes">
    <table class="MsoTableGrid" border="1" cellspacing="0" cellpadding="0" style='border-collapse:collapse;border:none'>
      <tr>
        <td width='576' colspan='6' valign='top' style='width:432.2pt;border:solid #C6D9F1 1.0pt;background:#C6D9F1;padding:0cm 5.4pt 0cm 5.4pt'>
          <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>
            <b>ALERTS</b>
          </p>
        </td>
      </tr>

      <xsl:choose>
        <xsl:when test="count(ReportTemplateDTO)>0">
          <xsl:apply-templates select='ReportTemplateDTO'/>
        </xsl:when>
        <xsl:otherwise>
          <tr>
            <td width='414' colspan='6' valign='top' style='width:310.2pt;border:solid #C6D9F1 1.0pt;border-top:none;padding:0cm 5.4pt 0cm 5.4pt'>
              <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>
                No alerts
              </p>
            </td>
          </tr>
        </xsl:otherwise>
      </xsl:choose>

      <tr height='0'>
        <td width='26' style='border:none'></td>
        <td width='28' style='border:none'></td>
        <td width='132' style='border:none'></td>
        <td width='227' style='border:none'></td>
        <td width='212' style='border:none'></td>
      </tr>
    </table>
  </xsl:template>

  <xsl:template match="RecomendationNotes">
    <table class="MsoTableGrid" border="1" cellspacing="0" cellpadding="0" style='border-collapse:collapse;border:none'>
      <tr>
        <td width='576' colspan='6' valign='top' style='width:432.2pt;border:solid #C6D9F1 1.0pt;background:#C6D9F1;padding:0cm 5.4pt 0cm 5.4pt'>
          <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>
            <b>RECOMMENDATIONS</b>
          </p>
        </td>
      </tr>

      <xsl:apply-templates select='ReportTemplateDTO'/>

      <tr height='0'>
        <td width='26' style='border:none'></td>
        <td width='28' style='border:none'></td>
        <td width='132' style='border:none'></td>
        <td width='227' style='border:none'></td>
        <td width='260' style='border:none'></td>
      </tr>
    </table>
  </xsl:template>

  <xsl:template match="ProblemNotes">
    <table class="MsoTableGrid" border="1" cellspacing="0" cellpadding="0" style='border-collapse:collapse;border:none'>
      <tr>
        <td width='576' colspan='6' valign='top' style='width:432.2pt;border:solid #C6D9F1 1.0pt;background:#C6D9F1;padding:0cm 5.4pt 0cm 5.4pt'>
          <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>
            <b>PROBLEMS</b>
          </p>
        </td>
      </tr>

      <xsl:apply-templates select='ReportTemplateDTO'/>

      <tr height='0'>
        <td width='26' style='border:none'></td>
        <td width='28' style='border:none'></td>
        <td width='132' style='border:none'></td>
        <td width='227' style='border:none'></td>
        <td width='212' style='border:none'></td>
      </tr>
    </table>
  </xsl:template>

  <xsl:template match="Reasons">
    <table class="MsoTableGrid" border="1" cellspacing="0" cellpadding="0" style='border-collapse:collapse;border:none'>
      <tr>
        <td width='576' colspan='6' valign='top' style='width:432.2pt;border:solid #C6D9F1 1.0pt;border-bottom:none;background:#C6D9F1;padding:0cm 5.4pt 0cm 5.4pt'>
          <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>
            <b>REASONS</b>
          </p>
        </td>
      </tr>
      <xsl:apply-templates select='CHSReasonReportInfo'/>
    </table>
  </xsl:template>

  <xsl:template match="CHSReasonReportInfo">
    <tr>
      <td width='92' valign='top' style='width:69.2pt;border-top:none;border-left:solid #C6D9F1 1.0pt;border-bottom:solid #C6D9F1 1.0pt;border-right:none;padding:0cm 5.4pt 0cm 5.4pt'>
        <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>
          <span style='font-size:9.0pt'>
            <xsl:value-of select ='Code'/>
          </span>
        </p>
      </td>
      <td width='132' valign='top' style='width:99.25pt;border:none;border-bottom:solid #C6D9F1 1.0pt;padding:0cm 5.4pt 0cm 5.4pt'>
        <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>
          <span style='font-size:9.0pt'>
            <xsl:value-of select ='Description'/>
          </span>
        </p>
      </td>
      <td width='352' valign='top' style='width:263.75pt;border-top:none;border-left:none;border-bottom:solid #C6D9F1 1.0pt;border-right:solid #C6D9F1 1.0pt;padding:0cm 5.4pt 0cm 5.4pt'>
        <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>
          <span style='font-size:9.0pt'>
            <xsl:value-of select ='Explanation'/>
          </span>
        </p>
      </td>
    </tr>
  </xsl:template>

  <xsl:template match="Visits/CHSMedicalEpisodeReportInfo">
    <p class='MsoNormal'>&#160;</p>
    <table class="MsoTableGrid" border="0" cellspacing="0" cellpadding="0" style='border-collapse:collapse;border:none;border-right:solid #C6D9F1 1.0pt;'>
      <tr>
        <td width='414' colspan='5' valign='top' style='width:310.2pt;border:none;border-bottom:solid #C6D9F1 1.0pt;background:#C6D9F1;padding:0cm 5.4pt 0cm 5.4pt'>
          <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>
            <b>
              <xsl:value-of select ='MedEpisodeActNumber'/>
            </b>
            : &#160;
            <xsl:value-of select ='MedicalEpisodeType'/>
          </p>
        </td>
        <td width='161' valign='top' style='width:120.45pt;border:none;border-bottom:solid #C6D9F1 1.0pt;background:#C6D9F1;padding:0cm 5.4pt 0cm 5.4pt'>
          <p class='MsoNormal' align='right' style='margin-bottom:0cm;margin-bottom:.0001pt;text-align:right;line-height:normal'>
            <b>
              <xsl:value-of select ='ms:format-date(EpisodeDateTime,"dd/MM/yyyy")'/>
            </b>
          </p>
        </td>
      </tr>
      <xsl:if test="PrimaryDiagnosisVisible='true' and (PrimaryDiagnosis/Code!='' or PrimaryDiagnosis/FullySpecifiedName!='')">
        <tr>
          <td width='116' colspan='3' valign='top' style='width:86.8pt;border:none;border-left:solid #C6D9F1 1.0pt;padding:0cm 5.4pt 0cm 5.4pt'>
            <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>
              <span style='font-size:9.0pt'>Primary diagnosis</span>
            </p>
          </td>
          <td width='458' colspan='3' valign='top' style='width:343.85pt;border:none;border-right:solid #C6D9F1 1.0pt;padding:0cm 5.4pt 0cm 5.4pt'>
            <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>
              <span lang='EN-US'>
                <xsl:value-of select ='PrimaryDiagnosis/Code'/>
                &#160;
                <xsl:value-of select ='PrimaryDiagnosis/FullySpecifiedName'/>
              </span>
            </p>
          </td>
        </tr>
      </xsl:if>
      <xsl:if test="SecondaryDiagnosisVisible='true'">
        <xsl:apply-templates select='SecondaryDiagnoses'/>
      </xsl:if>
      <xsl:if test="ClinicalNotesVisible='true'">
        <xsl:apply-templates select ='ClinicalNotes'/>
      </xsl:if>
      <xsl:if test="DiagnosticTestVisible='true'">
        <xsl:apply-templates select ='DiagnosticTestReportInfo'/>
      </xsl:if>
      <xsl:if test="TreatmentReportVisible='true'">
        <xsl:apply-templates select ='TreatmentReportInfo'/>
      </xsl:if>
      <xsl:if test="MonitoringTestVisible='true'">
        <xsl:apply-templates select ='MonitoringTestReportInfo'/>
      </xsl:if>
      <xsl:if test="EducationVisible='true'">
        <xsl:apply-templates select ='EducationReportInfo'/>
      </xsl:if>
      <xsl:if test="NurseEpisodeVisible='true'">
        <xsl:apply-templates select ='NurseEpisodeReportInfo'/>
      </xsl:if>

      <tr height='0'>
        <td width='26' style='border:none'></td>
        <td width='28' style='border:none'></td>
        <td width='61' style='border:none'></td>
        <td width='71' style='border:none'></td>
        <td width='227' style='border:none'></td>
      </tr>
    </table>
  </xsl:template>

  <xsl:template match="CHSMedicalEpisodeReportInfo/SecondaryDiagnoses/CHSCodifiedReportInfo">
    <tr>
      <td width='116' colspan='3' valign='top' style='width:86.8pt;border:none;border-left:solid #C6D9F1 1.0pt;padding:0cm 5.4pt 0cm 5.4pt'>
        <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>
          <span style='font-size:9.0pt'>Secondary diagnosis</span>
        </p>
      </td>
      <td width='458' colspan='3' valign='top' style='width:343.85pt;border:none;border-right:solid #C6D9F1 1.0pt;padding:0cm 5.4pt 0cm 5.4pt'>
        <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>
          <span lang='EN-US'>
            <xsl:value-of select ='Code'/>
            &#160;
            <xsl:value-of select ='FullySpecifiedName'/>
          </span>
        </p>
      </td>
    </tr>
  </xsl:template>

  <xsl:template match="ClinicalNotes">
    <tr>
      <td width='574' colspan='6' valign='top' style='width:430.65pt;border:solid #C6D9F1 1.0pt;border-top:none;padding:0cm 5.4pt 0cm 5.4pt'>
        <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>
          <span lang='EN-US'>&#160;</span>
        </p>
      </td>
    </tr>

    <tr>
      <td width='574' colspan='6' valign='top' style='width:430.65pt;border:solid #C6D9F1 1.0pt;border-top:none;padding:0cm 5.4pt 0cm 5.4pt'>
        <p class='MsoNormal' align='center' style='margin-bottom:0cm;margin-bottom:.0001pt;text-align:center;line-height:normal'>
          <span lang='EN-US' style='color:#548DD4'>
            CLINICAL NOTES
          </span>
        </p>
      </td>
    </tr>
    <xsl:apply-templates select='ReportNotes/ReportTemplateDTO'/>
  </xsl:template>

  <xsl:template match="DiagnosticTestReportInfo">
    <tr>
      <td width='574' colspan='6' valign='top' style='width:430.65pt;border:solid #C6D9F1 1.0pt;border-top:none;padding:0cm 5.4pt 0cm 5.4pt'>
        <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>
          <span lang='EN-US'>&#160;</span>
        </p>
      </td>
    </tr>

    <tr>
      <td width='574' colspan='6' valign='top' style='width:430.65pt;border:solid #C6D9F1 1.0pt;border-top:none;padding:0cm 5.4pt 0cm 5.4pt'>
        <p class='MsoNormal' align='center' style='margin-bottom:0cm;margin-bottom:.0001pt;text-align:center;line-height:normal'>
          <span lang='EN-US' style='color:#548DD4'>
            DIAGNOSTIC TESTS
          </span>
        </p>
      </td>
    </tr>

    <xsl:apply-templates select='CHSGenericReportInfo/ReportNotes/ReportTemplateDTO'/>
  </xsl:template>

  <xsl:template match="TreatmentReportInfo">
    <tr>
      <td width='574' colspan='6' valign='top' style='width:430.65pt;border:solid #C6D9F1 1.0pt;border-top:none;padding:0cm 5.4pt 0cm 5.4pt'>
        <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>
          <span lang='EN-US'>&#160;</span>
        </p>
      </td>
    </tr>

    <tr>
      <td width='574' colspan='6' valign='top' style='width:430.65pt;border:solid #C6D9F1 1.0pt;border-top:none;padding:0cm 5.4pt 0cm 5.4pt'>
        <p class='MsoNormal' align='center' style='margin-bottom:0cm;margin-bottom:.0001pt;text-align:center;line-height:normal'>
          <span lang='EN-US' style='color:#548DD4'>
            TREATMENTS
          </span>
        </p>
      </td>
    </tr>

    <xsl:apply-templates select='CHSGenericReportInfo/ReportNotes/ReportTemplateDTO'/>
  </xsl:template>

  <xsl:template match="MonitoringTestReportInfo">
    <tr>
      <td width='574' colspan='6' valign='top' style='width:430.65pt;border:solid #C6D9F1 1.0pt;border-top:none;padding:0cm 5.4pt 0cm 5.4pt'>
        <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>
          <span lang='EN-US'>&#160;</span>
        </p>
      </td>
    </tr>

    <tr>
      <td width='574' colspan='6' valign='top' style='width:430.65pt;border:solid #C6D9F1 1.0pt;border-top:none;padding:0cm 5.4pt 0cm 5.4pt'>
        <p class='MsoNormal' align='center' style='margin-bottom:0cm;margin-bottom:.0001pt;text-align:center;line-height:normal'>
          <span lang='EN-US' style='color:#548DD4'>
            MONITORING
          </span>
        </p>
      </td>
    </tr>

    <xsl:apply-templates select='CHSGenericReportInfo/ReportNotes/ReportTemplateDTO'/>
  </xsl:template>

  <xsl:template match="EducationReportInfo">
    <tr>
      <td width='574' colspan='6' valign='top' style='width:430.65pt;border:solid #C6D9F1 1.0pt;border-top:none;padding:0cm 5.4pt 0cm 5.4pt'>
        <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>
          <span lang='EN-US'>&#160;</span>
        </p>
      </td>
    </tr>

    <tr>
      <td width='574' colspan='6' valign='top' style='width:430.65pt;border:solid #C6D9F1 1.0pt;border-top:none;padding:0cm 5.4pt 0cm 5.4pt'>
        <p class='MsoNormal' align='center' style='margin-bottom:0cm;margin-bottom:.0001pt;text-align:center;line-height:normal'>
          <span lang='EN-US' style='color:#548DD4'>
            EDUCATION
          </span>
        </p>
      </td>
    </tr>

    <xsl:apply-templates select='CHSGenericReportInfo/ReportNotes/ReportTemplateDTO'/>
  </xsl:template>

  <xsl:template match="NurseEpisodeReportInfo">
    <tr>
      <td width='574' colspan='6' valign='top' style='width:430.65pt;border:solid #C6D9F1 1.0pt;border-top:none;padding:0cm 5.4pt 0cm 5.4pt'>
        <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>
          <span lang='EN-US'>&#160;</span>
        </p>
      </td>
    </tr>

    <tr>
      <td width='574' colspan='6' valign='top' style='width:430.65pt;border:solid #C6D9F1 1.0pt;border-top:none;padding:0cm 5.4pt 0cm 5.4pt'>
        <p class='MsoNormal' align='center' style='margin-bottom:0cm;margin-bottom:.0001pt;text-align:center;line-height:normal'>
          <span lang='EN-US' style='color:#548DD4'>
            NURSE
          </span>
        </p>
      </td>
    </tr>

    <xsl:apply-templates select='CHSGenericReportInfo/ReportNotes/ReportTemplateDTO'/>
  </xsl:template>

  <xsl:template match="ReportTemplateDTO">
    <tr>
      <td width='414' colspan='5' valign='top' style='width:310.2pt;border:solid #C6D9F1 1.0pt;border-top:none;padding:0cm 5.4pt 0cm 5.4pt'>
        <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>
          <xsl:value-of select ='Title'/>
        </p>
      </td>
      <td width='163' valign='top' style='width:122.0pt;border-top:none;border-left:none;border-bottom:solid #C6D9F1 1.0pt;border-right:solid #C6D9F1 1.0pt;padding:0cm 5.4pt 0cm 5.4pt'>
        <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;text-align:right;line-height:normal'>
          <b>
            <span lang='EN-US' style='font-size:9.0pt'>Fecha:&#160;</span>
          </b>
          <span lang='EN-US'>
            <xsl:value-of select ='DateTime'/>
          </span>
        </p>
      </td>
    </tr>

    <xsl:apply-templates select='Blocks'/>
  </xsl:template>

  <xsl:template match="Blocks/ReportBlockDTO">
    <tr>
      <td width='26' valign='top' style='width:19.6pt;border:none;border-left:solid #C6D9F1 1.0pt;padding:0cm 5.4pt 0cm 5.4pt'>
        <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>
          <span lang='EN-US'>&#160;</span>
        </p>
      </td>
      <td width='550' colspan='5' valign='top' style='width:412.6pt;border:none;border-left:solid #C6D9F1 1.0pt;border-right:solid #C6D9F1 1.0pt;border-bottom:solid #C6D9F1 1.0pt;padding:0cm 5.4pt 0cm 5.4pt'>
        <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>
          <b>
            <span lang='EN-US'>
              <xsl:value-of select ='Title'/>
            </span>
          </b>
        </p>
      </td>
    </tr>
    <tr>
      <td width='26' valign='top' style='width:19.6pt;border:none;border-left:solid #C6D9F1 1.0pt;border-bottom:solid #C6D9F1 1.0pt;padding:0cm 5.4pt 0cm 5.4pt'>
        <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>
          <span lang='EN-US'>&#160;</span>
        </p>
      </td>
      <td width='26' valign='top' style='width:19.6pt;border:none;border-bottom:solid #C6D9F1 1.0pt;padding:0cm 5.4pt 0cm 5.4pt'>
        <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>
          <span lang='EN-US'>&#160;</span>
        </p>
      </td>
      <td width='550' colspan='5' valign='top' style='width:412.6pt;border:none;border-right:solid #C6D9F1 1.0pt;border-bottom:solid #C6D9F1 1.0pt;padding:0cm 5.4pt 0cm 5.4pt'>
        <table class="MsoTableGrid" border="0" cellspacing="0" cellpadding="0" style='border-collapse:collapse;border:none'>
          <xsl:apply-templates select='BlockRows'/>
        </table>
      </td>
    </tr>
  </xsl:template>

  <xsl:template match='BlockRows/ReportBlockRowDTO'>
    <tr>
      <tr>
        <xsl:apply-templates select='Observations'/>
      </tr>
    </tr>
  </xsl:template>

  <xsl:template match='Observations/ReportObservationDTO'>
    <xsl:choose>
      <xsl:when test="Type='Label'">
        <td colspan='{ColSpan}' valign='top' style='border:none;border-left:solid #C6D9F1 1.0pt;padding:0cm 5.4pt 0cm 5.4pt'>
          <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>
            <span lang='EN-US' style='font-size:9.0pt'>
              <xsl:apply-templates select='Value'/>
            </span>
          </p>
        </td>
      </xsl:when>
      <xsl:otherwise>
        <td colspan='{ColSpan}' valign='top' style='border:none;border-left:solid #C6D9F1 1.0pt;padding:0cm 5.4pt 0cm 5.4pt'>
          <p class='MsoNormal' style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'>
            <b>
              <span lang='EN-US' style='font-size:9.0pt'>
                <xsl:choose>
                  <xsl:when test="ValueType='RichText'">
                    <xsl:value-of select='RawValue' disable-output-escaping="yes"/>
                  </xsl:when>
                  <xsl:when test="ValueType='Boolean'">
                    <xsl:if test="Value='true'">
                      <xsl:text>X</xsl:text>
                    </xsl:if>
                  </xsl:when>
                  <xsl:when test="ValueType='MultiMedia'">
                    <xsl:variable name="DocName" select="Value"/>
                    <img src="data:{/ClinicalHistorySummaryReportDTO/CHSummaryBody/Images/DocumentImageDTO[Name=$DocName]/MimeType};base64,{/ClinicalHistorySummaryReportDTO/CHSummaryBody/Images/DocumentImageDTO[Name=$DocName]/ImageData}"/>
                  </xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of select='Value'/>
                  </xsl:otherwise>
                </xsl:choose>
              </span>
            </b>
          </p>
        </td>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
</xsl:stylesheet>