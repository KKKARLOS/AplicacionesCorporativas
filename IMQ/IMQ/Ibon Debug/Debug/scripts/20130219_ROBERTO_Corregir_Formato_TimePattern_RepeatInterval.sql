update timepattern
set meaning = substring(meaning, 1, 6) + substring(meaning, 8, 1000)
where substring(meaning, 1, 7) = 'RI QDA1' and patterntype=8