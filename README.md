# VolumePercentage

This indicator allows you to detect how the volume is fluctuating according to the price, especially in relevant levels like support and resistance. The idea behind of that is to show what is the increment percentage on the volume bar respect to the previous volume bar. Also it paints the current volume bar in different colours depending if the bar is a quarter, second quarter, third quarter or fourth quarter from the previous bar, and it paints the bars above a certain value (fixed by the user, default = 30) in a different colour.

## Requirements
    Ninjatrader 8
    https://ninjatraderecosystem.com/user-app-share-download/universal-counter/  --> Recommended

## How to install?
Copy the file on:
    C:\Users\<user_name>\Documents\NinjaTrader 8\bin\Custom\Indicators
    
Then on NinjaTrader, New\NinjaScript Editor. Press the buttom: _Compile_
After that, the indicator will be in the Indicators menu. (Ctrl + I)

## Configuration (default)

<p align="center">
  <img src="https://github.com/rgutmen/VolumePercentage/blob/master/resources/VP_1.png" />
</p>

* Colour parameters
  * First Quarter: Volume bar colour, when the value is less than a quarter from the previous bar. => PV * (1/4) > CV
  * Second Quarter: Volume bar colour, when the value is less than a second quarter and higher than a quarter from the previous bar. => (PV * (1/4)) < CV < (PV * (2/4))
  * Third Quarter: Volume bar colour, when the value is less than a third quarter and higher than second quarter from the previous bar. => (PV * (2/4)) < CV < (PV * (3/4))
  * Fourth Quarter: Volume bar colour, when the value is less than a fourth quarter and higher than third quarter from the previous bar. => (PV * (3/4)) < CV < (PV * (4/4))
    * Current Volume: CV
    * Previous Volume: PV
  * Colour bullish candle: When the volume is above the value assigned by the user, and the candle is bullish. The candle and the volume bar are painted with this colour selected.
  * Colour bearish candle: When the volume is above the value assigned by the user, and the candle is bearish. The candle and the volume bar are painted with this colour selected.

* Parameters 
  * Percentage: Is the volume increment (in percentage, for example: 30, it means 30% of the volume)
  * RightBottom corner separation: is the amount of 'tabs' (Tab Key) to separate the 'percentage' (for every candle bar) from the right border.
   
## How it works?

The functionality is described on the next lines, about why the bars painted like they are in the image.

<p align="center">
  <img src="https://github.com/rgutmen/VolumePercentage/blob/master/resources/VP_3.png" />
</p>


1. Is **magenta** Because the volume at least 130% than the previous bar.
2. Is **orange** Because the volume is between 25% and 50% from the preivous bar.
3. Is **green** Because the volume is above 75% from the preivous bar.
4. Is **yellow** Because the volume is between 50% and 75% from the preivous bar.
5. Is **orange** Because the volume is between 25% and 50% from the preivous bar.
6. Is **yellow** Because the volume is between 50% and 75% from the preivous bar.
7. Is **yellow** Because the volume is between 50% and 75% from the preivous bar.
8. Is **orange** Because the volume is between 25% and 50% from the preivous bar.
9. Is **magenta** Because the volume at least 130% than the previous bar.

**The percentage 54.66% (in magenta above the candle bar), indicate accuratly what is the increment respect the previous bar, this means this volume bar is 54.66 times bigger than the previous one. This number is holded on the candle bar until the volume exceeds a 30% for a new candle bar.**

**For EACH candle bar, the volume percentage in relation to the previous bar is calculated. In the case of 154.66%, it means that the current bar is 154.66% higher than the previous one, but as soon as a new candle bar opens, the calculus starts for the new bar.**

## Functionality

   
## What I have learn?
I have learn about:
* C#
* Ninjatrader (Codding)
* Trading.

## Improvements
* Use the VWAP to colour de candles if the price is above or below, instead bullish and bearish.
