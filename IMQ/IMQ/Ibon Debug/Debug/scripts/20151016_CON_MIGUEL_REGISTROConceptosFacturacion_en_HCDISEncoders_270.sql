USE [HCDISEncoders]
GO

/****** Object:  Table [dbo].[conceptosfacturacion] Script Date: 10/16/2015 12:10:08 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[conceptosfacturacion]') AND type in (N'U'))
DROP TABLE [dbo].[conceptosfacturacion]
GO

USE [HCDISEncoders]
GO

/****** Object:  Table [dbo].[conceptosfacturacion] Script Date: 10/16/2015 12:10:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[conceptosfacturacion](
	[CODIGO] [nvarchar](255) NULL,
	[DESCRIPCION] [nvarchar](255) NULL,
	[TIPO] [nvarchar](255) NULL,
	[CUENTA] [nvarchar](255) NULL
) ON [PRIMARY]

GO


INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('CHQH3','CHEQUEO MEDICO HOMBRE + DE 46 A','CHEQUEO','70500003')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('CHQH2','CHEQUEO MEDICO HOMBRE DE 31 A 4','CHEQUEO','70500003')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('CHQH1','CHEQUEO MEDICO HOMBRE HASTA 30','CHEQUEO','70500003')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('CHQ','CHEQUEO MEDICO MUJER + 46','CHEQUEO','70500003')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('CHQM2','CHEQUEO MEDICO MUJER DE 31 A 39','CHEQUEO','70500003')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('CHQM3','CHEQUEO MEDICO MUJER DE 40 A 45','CHEQUEO','70500003')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('CHQM1','CHEQUEO MEDICO MUJER HASTA 30 A','CHEQUEO','70500003')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('CHQA','CHEQUEO M?DICO EMPRESAS O DEPORTIVO AMPLIADO','CHEQUEO','70500003')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('60933A','TTO AMBULATORIO POR QUIMIOTERAPIA','ONCO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('100923A','TRATAMIENTO ESPECIAL VIA I.V. SIN INGRESO','ONCO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('300800P','LIMPIEZA DE RESERVORIO','ONCO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('400911A','TTO. DEL DOLOR-QUIROFANO AMBULATORIO','ESTAN','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('420000A','HONORARIOS RADIOTERAPIA','HONOR','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('420002A','PLANIFICACION TRATAMIENTO DE RADIOTERAPIA','ONCO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('420003A','SESIONES TRATAMIENTO DE RADIOTERAPIA','ONCO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('GRAPOTRA','OTRAS SUTURAS MECANICAS','MATERIAL','70500006')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('FORQUI','FORFAIT ESTANCIA QUIRURGICA','ESTAN','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('FORMED','FORFAIT ESTANCIA MEDICA','ESTAN','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('FOR24Q','FORFAIT QUIRURGICO 24 HORAS','ESTAN','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('ONCING','QUIMIOTERAPIA CON INGRESO','ESTAN','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('UCI','FORFAIT DE ESTANCIA EN UCI.','ESTAN','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('GAMMA','VACUMA GAMMA ANTI D','FARMA','70500007')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('PREPI','PREPIDIL GEL','FARMA','70500007')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('TISSU','TISSUCOL','FARMA','70500007')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('COM','HONORARIOS COMADRONA','HONOR','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('HEMO','HONORARIOS HEMODINAMICA','HONOR','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('HEND','HONORARIOS ENDOSCOPIAS','HONOR','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('HONOR','HONORARIOS MEDICOS','HONOR','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('LAB','HONORARIOS LABORATORIO','HONOR','70500005')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('RAD','HONORARIOS RADIOLOGIA','HONOR','70500004')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('UTD','HONORARIOS U.T.D.','HONOR','70500003')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('GRA','GRAPADORAS LINEALES RECTAS RECA','MATERIAL','70500006')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('GRAEXT','EXTRACTORES DE GRAPAS','MATERIAL','70500006')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('GRALIREAR','GRAPADORAS LINEALES RECTAS ARTI','MATERIAL','70500006')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('GRAPCIRCU','GRAPADORAS CIRCULARES CURVAS','MATERIAL','70500006')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('GRAPCIRRE','GRAPADORAS CIRCULARES RECTAS','MATERIAL','70500006')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('GRAPCORTRE','GRAPADORAS CORTADORAS RECARGABL','MATERIAL','70500006')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('GRAPENDO','ENDOGRAPADORAS CIR. ABIERTA Y L','MATERIAL','70500006')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('GRAPIEL','GRAPADORAS DE PIEL','MATERIAL','70500006')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('GRAPLIRERE','GRAPADORAS LINEALES RECTAS RECA','MATERIAL','70500006')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('GRAPUSTR','PURSTRING INSTRUMENTO BOLSA TAB','MATERIAL','70500006')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('LIGA','LIGASURE','MATERIAL','70500006')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('MALLA','MALLA PREFORMADA CON TAPON  980','MATERIAL','70500006')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('MALLA01','MALLA PLANA DE POLIPROPILENO 98','MATERIAL','70500006')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('MALLA02','MALLAS BILAMINARES EVENTRACION','MATERIAL','70500006')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('MALLA03','MALLAS OTRAS 980115','MATERIAL','70500006')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('MAT','MATERIALES ESPECIALES','MATERIAL','70500006')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('MURONEFPER','MAT. NEFROLITOTOMIA PERCUTANEA','MATERIAL','70500006')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('MURONEFRAD','MAT. NEFRECTOMIA RAD POR LAPARO','MATERIAL','70500006')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('MURONPDES','MAT. NEFROSTOMIA PERCUTANEA DES','MATERIAL','70500006')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('MUROPIELO','MAT. PIELOPLASTIA PIELO-URETRAL','MATERIAL','70500006')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('MUROPRORA','MAT. PROSTATECTOMIA RADICAL POR','MATERIAL','70500006')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('UROVAPOR','TERMINAL RESECCION VAPORIZACION','MATERIAL','70500006')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('MED','MEDICACION','FARMA','70500007')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('MON','MEDICACION ONCOLOGIA','FARMA','70500007')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('MPL','MEDICACION Y MATERIAL PLANTAS','FARMA','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('MUC','MEDICACION Y MATERIAL UCI','FARMA','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('OZO','HONORARIOS OZONOTERAPIA','HONOR','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('ECC','ECOCARDIOGRAMA','RADIO','70500004')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('ESF','PRUEBA DE ESFUERZO','RADIO','70500004')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('HOLT','HOLTER','RADIO','70500004')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('RMN','RESONANCIA MAGNETICA','RADIO','70500004')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('RMN2','RESONANCIA MAGNETICA DOBLE','RADIO','70500004')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('TAC','TAC CRANEAL','RADIO','70500004')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('AMB','FORFAIT AMBULATORIO','ESTAN','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('DQU','DERECHOS DE QUIROFANO','ESTAN','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('REINT','REINTERVENCION','ESTAN','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('TVQ','TV QUIROFANO ESCOPIA','ESTAN','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('TRACE','TRATAMIENTO ACELERADOR L. COMPL','ONCO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('VAR','VARIOS','VARIOS','70900000')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('VARIVA','VARIOS 8% IVA','VARIOS','70900000')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('DEN','DENSITOMETRIA OSEA','RADIO','70500004')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('DOP','DOPPLER','RADIO','70500004')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('ECO','ECOGRAF?A','RADIO','70500004')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('ECOAP','ECOGRAFIA ABDOMINO-PELVICA','RADIO','70500004')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('ECOBS','ECOGRAFIA OBSTETRICA','RADIO','70500004')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('ECOBS20','ECOGRAFIA OBSTETRICA 20 SEMANAS','RADIO','70500004')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('ECODOP','ECOGRAFIA DOPPLER','RADIO','70500004')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('ECOES','ECOGRAFIA TRANSRECTAL','RADIO','70500004')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('ARTHRO','ARTHROCARE','MATERIAL','70500006')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('ASPI','ASPIRADOR ULTRASONICO','MATERIAL','70500006')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('BISTU','BISTURI ARMONICO','MATERIAL','70500006')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('MUROCAL','CALCULO COLARIFORME TRATAMIENTO','MATERIAL','70500006')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('CARGAPLI','CARGA APLICADORES DE CLIPS','MATERIAL','70500006')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('CARGRALIRA','CARGA GRAPADORA LINEAL RECTA AR','MATERIAL','70500006')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('CARGRACORT','CARGA PARA GRAPADORAS CORTADORA','MATERIAL','70500006')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('CARGENDART','CARGAS ARTICULADAS ENDOGRAPADOR','MATERIAL','70500006')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('CARGRALIRE','CARGAS GRAPADORAS LINEALES RECT','MATERIAL','70500006')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('CARGENDRE','CARGAS RECTAS ENDOGRAPADORAS','MATERIAL','70500006')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('UTDAMB','UNIDAD DEL DOLOR AMBULATORIA','ESTAN','70500003')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('TFN','TELEFONO','VARIOS','70900000')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('TV','TELEVISION','VARIOS','70900000')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('CANAL PLUS','CANAL + LIGA O ESPECIAL','VARIOS','70900000')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('VIDEO','VIDEOTECA 24 H','VARIOS','70900000')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('WIFI','WIFI','VARIOS','70900000')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('UTDING','UNIDAD DEL DOLOR CON INGRESO','ESTAN','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('NEONATO','U.NEONATOS CUIDADOS INTERMEDIOS','ESTAN','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('980205','MATERIALES ESPECIALES','MATERIAL','70500006')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('ENF','HONORARIOS ENFERMERIA','HONOR','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('420000S','SALA RADIOTERAPIA','ONCO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('URG','VISITA DE URGENCIA','URGEN','70500002')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('CURA','CURAS DE HERIDAS','URGEN','70500002')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('ESGUI','ESGUINCE','URGEN','70500002')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('PED','URGENCIAS PEDIATRIA','URGEN','70500002')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('CONSEG','CONSULTA SEGURO ESCOLAR','URGEN','70500002')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('URGGINE','URGENCIA GINECOLOGIA Y OBSTETRICIA','URGEN','70500002')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('663A','TORACOCENTESIS','ENDOS','70500000')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('10313A','ESFINTEROTOMIA ENDOSCOPICA','ENDOS','70500000')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('10531A','POLIPECTOMIA INTESTINAL ENDOSCOPICA','ENDOS','70500000')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('10533A','ESCLEROSIS VARICES ESOFAGICAS VIA ENDOSCOPICA','ENDOS','70500000')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('10538A','POLIPECTOMIA GASTRICA ENDOSCOPICA','ENDOS','70500000')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('10545A','BANDAS ELASTICAS VARICES ESOFAG.V/ENDOSCOPICA','ENDOS','70500000')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('10549A','POLIPECTOMIA MULTIPLE-P.GIGANTE V/ENDOSCOPICA','ENDOS','70500000')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('10623A','DILATACIONES ESOFAGICAS -PRIMERA-','ENDOS','70500000')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('10624A','CUERPO EXTRA/O ESOFAGICO VIA ENDOSCOPICA','ENDOS','70500000')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('10712A','DILATACIONES ESOFAGICAS SIGUIENTES','ENDOS','70500000')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('10903A','GASTROSCOPIA','ENDOS','70500000')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('10909A','ESOFAGOSCOPIA','ENDOS','70500000')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('10911A','COLONOSCOPIA RECTOSIGMOIDOSCOPIA. BIOPSIA','ENDOS','70500000')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('10913A','C.P.R.E. TERAPEUTICA','ENDOS','70500000')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('10915A','PROTESIS ESOFAGICA.COLOCACION ENDOSCOPICA','ENDOS','70500000')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('10918A','CAPSULA ENDOSCOPICA','ENDOS','70500000')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('20703A','FIBROBRONCOSCOPIA','ENDOS','70500000')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('200550A','TECNICAS HEMOSTATICAS ENDOSCOPICAS','ENDOS','70500000')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('200914A','GASTROSTOMIA ENDOSCOPICA PERCUTANEA','ENDOS','70500000')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('200922A','ESTANCIA AMBULATORIA POST-SEDACION','ENDOS','70500000')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('200924A','ECOENDOSCOPIA DIAGNOSTICA BAJA (ANO-RECTAL)','ENDOS','70500000')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('200925A','ECOENDOSCOPIA DIAGNOSTICA ALTA','ENDOS','70500000')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('200926A','ECOENDOSCOPIA TERAPEUTICA-C/PUNCION','ENDOS','70500000')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('4233A','POLIPECTOMIA (INTESTINAL Y GASTRICA)','ENDOS','70500000')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('4294A','DILATACI?N C/ BAL?N HIDRONEUMATICO V/ENDOSCOPICA','ENDOS','70500000')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('4523A','COLONOSCOPIA','ENDOS','70500000')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('BALINTCOL','BALON INTRAGASTRICO COLOCACI?N','ENDOS','70500000')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('BALINTEXT','BALON INTRAGASTRICO EXTRACCI?N','ENDOS','70500000')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('END','FORFAIT AMBULATORIO ENDOSCOPIA','ENDOS','70500000')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('4233A','POLIPECTOMIA (Intestinal y gastrica)','ENDOS','70500000')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('11A','BY-PASS GASTRICO-C.BARIATRICA','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('214A','HEMICOLECTOMIA DERECHA O IZQUIERDA','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('303A','COLECISTECTOMIA POR LAPAROSCOPIA','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('306A','HERNIA DE HIATO POR LAPAROSCOPIA','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('415A','COLECISTECTOMIA','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('501A','HERNIA BILATERAL','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('522A','APENDICECTOMIA','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('532A','HERNIA INGUINAL,CRURAL O UMBILICAL','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('10010A','AMPUTACION O RESEC.ANTERIOR BAJA POR CA RECTO','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('10543A','HERNIA VENTRAL POSTLAPARATOMIA / LAPAROSCOPIA','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('20104A','INTERVENCIONES POR VIDEOTORACOSCOSPIA Y CIRUG','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('40102A','ARTROPLASTIA PARCIAL CADERA, CERVICO-CEFALICAS','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('40107A','PROTESIS TOTAL DE RODILLA','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('40429A','ENFERMEDAD DUPUYTREN,SINDACTILIA','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('40521A','NUCLEOTOMIA PERCUTANEA','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('40522A','MENISCECTOMIA POR ARTROSCOPIA','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('40523A','HALLUX VALGUS BILATERAL CON DEDO EN MARTILLO','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('40620A','HALLUX VALGUS UNILATERAL CON DEDO EN MARTILLO','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('40705A','TENOTOMIAS','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('40901A','ARTROSCOPIA','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('50235A','RINOSEPTOPLASTIA FUNCIONAL','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('50310A','COLGAJOS MEDIANOS','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('50313A','GINECOMASTIA','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('50315A','INJERTO PRECOZ MANO, GRANDE','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('50338A','TUMORES LABIO, PARPADOS, EXTIRPACION Y RECONSTR','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('60200A','PROSTATECTOMIA EN UN TIEMPO','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('60409A','BIOPSIA VESICAL RANDOMIZADA','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('60500A','EXTIRPACION ENDOSCOPICA TUMORES','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('60521A','OPERACIONES URETERALES URETERO-RENOSCOPIO','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('60616A','BIOPSIA PUNCION PROSTATICA ECODIRIGIDA','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('60711A','CIRCUNCISION POR FIMOSIS','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('60721A','DEFERENTOMIA BILATERAL TRAT.UNICO','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('60805A','PUNCION BIOPSIA PROSTATA','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('70120A','HISTERECTOMIA TOTAL(ABDOM.VAGIN.CON O SIN ANE','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('70222A','HISTERECTOMIA SUB TOTAL','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('70300A','CESAREA','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('70310A','MIOMECTOMIAS','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('70500A','PARTO NORMAL Y PREMATURO','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('70701A','LEGRADO UTERINO','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('90111A','TIMPANOPLASTIA Y ESTAPEDECTOMIA','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('90114A','CIRUGIA ENDOSCOPICA NASAL','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('90224A','DESVIACION NARIZ. CORRECCION FUNCIONAL','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('90424A','RESECCION SUBMUCOSA DE TABIQUE','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('90426A','MICROCIRUGIA LARINGEA','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('90428A','AMIGDALAS','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('90524A','AMIG. Y VEGET. CON O SIN DREN','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('90526A','DRENAJES Y VEGETACIONES','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('90631A','MICRODRENAJE TIMPANICO','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('90731A','VEGETACIONES','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('100000A','VITRECTOMIA POSTERIOR POR PARS.PLANA','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('100003A','DESPRENDIMIENTO DE RETINA CON VITRECTOMIA','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('100010A','DESPRENDIMIENTO DE RETINA TRAT COMPLET','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('100020A','CATARATA CON IMPLANTE PROTESIS','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('100200A','TRABECULECTOMIA Y SIMILARES','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('140000A','REVASC. ANEURISMA GRANDES TRONCOS','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('140302A','SAFENECTOMIA BILATERAL POR TECNICA CHIVA','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('140310A','SAFENECTOMIA BILATERAL','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('140508A','SAFENECTOMIA UNILATERAL POR TECNICA CHIVA','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('140510A','SAFENECTOMIA UNILATERAL','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('150542A','QUISTES MAXILARES CON INGRESO','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('150621A','PIEZAS DENTARIAS INCLUIDAS -CANINO- CON INGRE','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('180021A','PONTAJE AORTO-CORONARIO SIMPLE','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('180025A','SUSTITUCION VALVULA (MITRAL,AORTICA,MITRAL PLASTIA TRICUSPIDEA','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('180026A','SUSTITUCION VALVULA AORTICA','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('180027A','SUSTITUCION VALVULA DOBLE MITRAL-AORTICA','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('180028A','SUST. VALVULA MITRO-AORTICA PLAST.TRICUSPIDE','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('180029A','SUST.VALVULA MITRAL PLASTIA TRICUSPIDEA','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('180030A','RESECCION SUBAORTICA','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('180031A','PLASTIA VALVULA MITRAL','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('180032A','ANEURISMA AORTA ASCEND. + VALVULA AORTICA','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('180033A','SUSTITUCION VALVULA + PONTAJE AORTO-CORONARIO','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('180034A','COMUNICACION INTERAURICULAR','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('180035A','ROTURAS CARDIACAS TRAUMATICAS','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('180036A','ANEURISMAS SIN C.E.C.','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('4946A','HEMORROIDECTOMIA','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('4959A','FISURA ANAL','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('6021A','PROSTATECTOMIA POR LASER','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('8100A','ARTRODESIS DE COLUMNA','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('8621A','QUISTE SACRO','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('863A','LIPOMA','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('9623A','DILATACION ANAL','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('PROMGD5','PROCESO GRUPO 5 MGD TRAUMA','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('PROUTE','PROCESOS UTE (CAMBIAR DESCRIPCI','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('8026A','ARTROSCOPIA RODILLA','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('2319A','CORDAL IMPACTADO CON INGRESO-PR','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('GASTR.LAP','GASTROPLASTIA LAPAROSCOPICA','PROCESO','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('SUPLEM','ESTANCIA EN SUITE SUPLEMENTO','ESTAN','70900000')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('URPA','ESTANCIA EN U.R.P.A.','ESTAN','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('HAB','ESTANCIA HABITACION','ESTAN','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('HDD','ESTANCIA HOSPITAL DE DIA','ESTAN','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('HON','ESTANCIA ONCOLOGIA','ESTAN','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('HUD','ESTANCIA U.T.D.','ESTAN','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('MATEND','MATERIAL ENDOSCOPIA','ENDOS','70500006')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('ALER','HONORARIOS PRUEBAS ALERGICAS','HONOR','70500003')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('PREOPOSA','PREOPERATORIOS OSAKIDETZA','HONOR','70500003')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('ANES','HONORARIOS ANESTESIA','HONOR','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('ANAPA','HONORARIOS ANATOMIA PATOLOGICA','HONOR','70500001')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('9000096000','REHABILITACION COMBINADA AMBULATORIA','REHA','70500009')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('9000096001','ULTRASONIDO','REHA','70500009')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('9000096002','ONDA CORTA','REHA','70500009')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('9000096003','ELECTROTERAPIA','REHA','70500009')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('9000096004','MICROONDAS','REHA','70500009')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('9000096005','SESIONES REHABI. TRAFICO','REHA','70500009')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('9000096006','REHAB.COMB AMB >20 SESION','REHA','70500009')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('9000096007','REHABILITACION EN CLINICA','REHA','70500009')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('9000096008','SESIONES COMBINADAS','REHA','70500009')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('9000096009','DIFERENCIA SESIONES REHABILITACION','REHA','70500009')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('9000096018','REHABILITACION LINFOEDEMA','REHA','70500009')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('9000096019','REHABILITACION DE SUELO PELVICO','REHA','70500009')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('9000096020','REHABILITACION RESPIRATORIA','REHA','70500009')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('9000096992','PRIMERA CONSULTA (REHABILITACION)','REHA','70500009')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('9000096993','CONSULTA SUCESIVA REHABILITACION','REHA','70500009')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('90000P3808','FISIOTERAPIA','REHA','70500009')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('ULTRAVIO','R.ULTRAVIOLESTAS','REHA','70500009')
INSERT INTO [conceptosfacturacion]([CODIGO],[DESCRIPCION],[TIPO],[CUENTA]) VALUES('MQU','MAT Y MEDICACION QUIROFANO','FARMA','70500001')
