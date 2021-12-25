# KanjiVG.Animation
Generates animated SVG files for [KanjiVG](https://github.com/KanjiVG/kanjivg) files

![kanji](samples/6f22.svg)
![kanji](samples/5b57.svg)

## Running the animator
```console
MyNihongo.KanjiVG.Animation -s "source path" -d "destination path"
```

#### List of args
|Name|Mandatory|Meaning|Default value|
|-|-|-|-|
|-s or -source|Yes|Path to the directory with KanjiVG files|
|-d or -destination|Yes|Path to the directory where animated KanjiVG are saved|
|-r or -rounding|No|Precision of double fields|3|
|-outerStroke|No|Brush of the outer (border) stroke|#666|
|-outerStrokeWidth|No|Width of the outer (border) stroke|6|
|-innerStroke|No|Brush of the inner (kanji) stroke|#000|
|-innerStrokeWidth|No|Width of the inner (kanji) stroke|3|