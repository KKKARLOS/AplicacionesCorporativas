USE [DBNAME]
--/////////////////////////////////////////////////////////////////////////////////////////
--////// script para actualizar el carecenterid en las facturas que ya estén emitidas /////
--/////////////////////////////////////////////////////////////////////////////////////////
UPDATE Invoice SET CareCenterID = CP.CareCenterID
FROM Invoice 
JOIN InvoiceEntry IE ON Invoice.ID = IE.InvoiceID
JOIN DeliveryNote DN ON IE.DeliveryNoteID = DN.ID
JOIN CustomerDeliveryNote CDN ON DN.CustomerDeliveryNoteID = CDN.ID
JOIN CustomerProcess CP ON CDN.CustomerEpisodeID = CP.CustomerEpisodeID