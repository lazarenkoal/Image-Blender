CV_Homework
===========

Image Blender
Доброго времени суток!

Сделал я таки этот блендер. То, о чем я писал в письме было ликвидированно исправлением
функции Expand. Однако, из-за блюра, версия для печати на экран выглядит немного смазанной,
чисто теоретически, это можно поправить деблюром через Wiener filter. Так как времени осталось
чуть меньше, чем нисколько, я писать это не стал.

Метод, который я пытался применить первым был метод пуассона.
Там я хотел тупо нагенерить матрицу разряженную и решить уравнение, но компьютер выдал мне
как-то раз OutOfMemoryException, после чего я бросил эту затею и перешел к пирамидкам.

Стало понятно как это делать руками достаточно быстро, но из-за всяких побочных вмешательств
реализация потребовала некоторого количества времени, но зато я разобрался с этими пирамидами и 
понял, что они достаточно крутые.

Метод, который я применяю для блюра очень медленный и топорный, потому что та версия, которую 
я написал для двумерного массива как-то сдвигала пиксели и все очень-очень сильно искажалось после
коллапса, поэтому я стал использовать эту версию, потому что она ничего не сдвигала.

P.S С наступающим новым годом;) Пусть все будет круто!
Лазаренко Александр, 1 курс, ПИ.
30.12.2014
