# VolumePercentage

This indicator allows you to detect how the volume is fluctuating according to the price, especially in relevant levels like support and resistance. The idea behind of that is to show what is the increment percentage on the volume bar respect to the previous volume bar. Also it paints the current volume bar in different colors depending if the bar is a quarter, two quarter, three quarter o four quarter from the prvious bar, and it paints the bars above a certain value (fixed by the user, default = 30) in a different color.

## Configuration

<p align="center">
  <img src="https://github.com/rgutmen/VolumePercentage/blob/master/resources/VP_1.png" />
</p>

* Color parameters
  * First Quarter: Volume bar color, when the value is less than a quarter from the previous bar. => PV * (1/4) > CV
  * Second Quarter: Volume bar color, when the value is less than a second quarter and higher than a quarter from the previous bar. => (PV * (1/4)) < CV < (PV * (2/4))
  * Third Quarter: Volume bar color, when the value is less than a third quarter and higher than second quarter from the previous bar. => (PV * (2/4)) < CV < (PV * (3/4))
  * Fourth Quarter: Volume bar color, when the value is less than a fourth quarter and higher than third quarter from the previous bar. => (PV * (3/4)) < CV < (PV * (4/4))
    * Current Volume: CV
    * Previous Volume: PV
  * Color bullish candle: When the volume is above the value assigned by the user, and the candle is bullish. The candle and the volume bar are painted with this color selected.
  * Color bearish candle: When the volume is above the value assigned by the user, and the candle is bearish. The candle and the volume bar are painted with this color selected.

* Parameters 
  * Percentage: Is the volume increment (in percentage, for example: 30, it means 30% of the volume)
   
## How it works?

Using the image below as example. The functionality is described on the next lines.

<p align="center">
  <img src="https://github.com/rgutmen/VolumePercentage/blob/master/resources/VP_2.png" />
</p>


Bar 1. Is **magenta** Because the volume at least 130% than the previous bar.
Bar 2. Is **orange** Because the volume is between 25% and 50% from the preivous bar.
Bar 3. Is **green** Because the volume is above 75% from the preivous bar.
Bar 4. Is **yellow** Because the volume is between 50% and 75% from the preivous bar.
Bar 5. Is **orange** Because the volume is between 25% and 50% from the preivous bar.
Bar 6. Is **yellow** Because the volume is between 50% and 75% from the preivous bar.
Bar 7. Is **yellow** Because the volume is between 50% and 75% from the preivous bar.
Bar 8. Is **orange** Because the volume is between 25% and 50% from the preivous bar.
Bar 9. Is **magenta** Because the volume at least 130% than the previous bar.

**The percentage 54.66% (in magenta above the candle bar), indicate accuratly what is the increment respect the previous bar, this means this volume bar is 54.66 times bigger than the previous one**








## Functionality

   
## What I have learn?
I have learn about:
* C#
* Ninjatrader (Codding)
* Trading.

## Improvements
* Use the VWAP to color de candles, instead bullish and bearish.
