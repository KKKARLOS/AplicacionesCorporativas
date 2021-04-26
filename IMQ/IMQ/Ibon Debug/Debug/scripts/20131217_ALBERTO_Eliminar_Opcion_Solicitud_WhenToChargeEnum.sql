
UPDATE ServiceCharge SET WhenToCharge=8
WHERE (WhenToCharge IN (2,16))

UPDATE ServiceCharge SET WhenToCharge=128
WHERE (WhenToCharge IN (32,256))
