//
// Author: Ruben Gutierrez
// Indicator for NinjaTrader 8
//

#region Using declarations
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.Gui;
using NinjaTrader.Gui.Chart;
using NinjaTrader.Gui.SuperDom;
using NinjaTrader.Gui.Tools;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.NinjaScript.DrawingTools;
#endregion

//This namespace holds Indicators in this folder and is required. Do not change it. 
namespace NinjaTrader.NinjaScript.Indicators {
    public class VolumePercentageNT8 : Indicator {
        protected override void OnStateChange() {
            if (State == State.SetDefaults) {
                Description = @"This indicator represents the current volume with a color according the previous volume bar.";
                Name = "VolumePercentageNT8";
                Calculate = Calculate.OnEachTick;
                IsOverlay = false;
                DisplayInDataBox = true;
                DrawOnPricePanel = true;
                DrawHorizontalGridLines = true;
                DrawVerticalGridLines = true;
                PaintPriceMarkers = true;
                ScaleJustification = NinjaTrader.Gui.Chart.ScaleJustification.Right;
                //Disable this property if your indicator requires custom values that cumulate with each new market data event. 
                //See Help Guide for additional information.
                IsSuspendedWhileInactive = true;
                Percentage = 30;
                tabTimes = 2;
                BullishCandle = Brushes.Cyan;
                BearishCandle = Brushes.Magenta;
                Quarter_1 = Brushes.Red;
                Quarter_2 = Brushes.Orange;
                Quarter_3 = Brushes.Yellow;
                Quarter_4 = Brushes.Lime;

                AddPlot(new Stroke(Brushes.White, 8), PlotStyle.Bar, "Plot0");
                Plots[0].AutoWidth = true;


            } else if (State == State.Configure) {
            }
        }

        protected override void OnBarUpdate() {

            if (BarsInProgress != 0)
                return;

            if (CurrentBars[0] < 1)
                return;

            Plot0[0] = Volume[0];


            double volAnteriorIncrementado = Volumes[0][1] * (1 + (Percentage / 100.0));

            double volpercentajeLeft = (Volumes[0][0] * 100.00) / Volumes[0][1];

          
            Draw.TextFixed(this, "PercentajeVolume", Convert.ToString(Math.Round(volpercentajeLeft, 2) + "%" + String.Concat(Enumerable.Repeat("\t", tabTimes))), TextPosition.BottomRight);


            if (volpercentajeLeft < ((100 + Percentage) / 4)) {
                PlotBrushes[0][0] = Quarter_1;
            }

            if (volpercentajeLeft > ((100 + Percentage) / 4) && volpercentajeLeft < (((100 + Percentage) * 2) / 4)) {
                PlotBrushes[0][0] = Quarter_2;
            }
            if (volpercentajeLeft > (((100 + Percentage) * 2) / 4) && volpercentajeLeft < (((100 + Percentage) * 3) / 4)) {
                PlotBrushes[0][0] = Quarter_3;
            }
            if (volpercentajeLeft > (((100 + Percentage) * 3) / 4) && volpercentajeLeft < (100 + Percentage)) {
                PlotBrushes[0][0] = Quarter_4;
            }

            //HIGH VOLUME SPIKE LONG SIGNAL
            if (volAnteriorIncrementado < Volumes[0][0]) {
                Draw.Text(this, "PercentajeOnBar", Convert.ToString(Math.Round(volpercentajeLeft, 2) - 100 + "%"), 0, (Low[0] - 10 * TickSize), Brushes.Cyan);
                //BackBrush = Brushes.Lime;
                BarBrushes[0] = BullishCandle;
                PlotBrushes[0][0] = BullishCandle;
            }


            //HIGH VOLUME SPIKE SHORT SIGNAL
            if (volAnteriorIncrementado < Volumes[0][0]) {
                //BackBrush = Brushes.Red;
                Draw.Text(this, "PercentajeOnBar", Convert.ToString(Math.Round(volpercentajeLeft, 2) - 100 + "%"), 0, (High[0] + 10 * TickSize), Brushes.Magenta);
                BarBrushes[0] = BearishCandle;
                PlotBrushes[0][0] = BearishCandle;
            }




        }

        #region Properties
        [NinjaScriptProperty]
        [Range(1, int.MaxValue)]
        [Display(Name = "Percentage", Description = "Percentaje", Order = 0, GroupName = "Parameters")]
        public int Percentage { get; set; }

        [NinjaScriptProperty]
        [Range(1, int.MaxValue)]
        [Display(Name = "RightBottom corner separation", Description = "RightBottom corner separation", Order = 1, GroupName = "Parameters")]
        public int tabTimes { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        public Series<double> Plot0 {
            get { return Values[0]; }
        }

        // Create our user definable color input
        [XmlIgnore()]
        [Display(Name = "First Quarter", Description = "Volume bar color when it reach the 25% from the previous bar.", GroupName = "Color Parameters", Order = 1)]
        public Brush Quarter_1 { get; set; }

        // Serialize our Color object
        [Browsable(false)]
        public string Quarter_1_Serialize {
            get { return Serialize.BrushToString(Quarter_1); }
            set { Quarter_1 = Serialize.StringToBrush(value); }
        }

        [XmlIgnore()]
        [Display(Name = "Second Quarter", Description = "Volume bar color when it reach the 50% from the previous bar.", GroupName = "Color Parameters", Order = 2)]
        public Brush Quarter_2 { get; set; }

        // Serialize our Color object
        [Browsable(false)]
        public string Quarter_2_Serialize {
            get { return Serialize.BrushToString(Quarter_2); }
            set { Quarter_2 = Serialize.StringToBrush(value); }
        }

        [XmlIgnore()]
        [Display(Name = "Third Quarter", Description = "Volume bar color when it reach the 75% from the previous bar.", GroupName = "Color Parameters", Order = 3)]
        public Brush Quarter_3 { get; set; }

        // Serialize our Color object
        [Browsable(false)]
        public string Quarter_3_Serialize {
            get { return Serialize.BrushToString(Quarter_3); }
            set { Quarter_3 = Serialize.StringToBrush(value); }
        }

        [XmlIgnore()]
        [Display(Name = "Fourth Quarter", Description = "Volume bar color when it reach the 100% from the previous bar.", GroupName = "Color Parameters", Order = 4)]
        public Brush Quarter_4 { get; set; }

        // Serialize our Color object
        [Browsable(false)]
        public string Quarter_4_Serialize {
            get { return Serialize.BrushToString(Quarter_4); }
            set { Quarter_4 = Serialize.StringToBrush(value); }
        }

        [XmlIgnore()]
        [Display(Name = "Color bearish candle", GroupName = "Color Parameters", Order = 6)]
        public Brush BearishCandle { get; set; }

        // Serialize our Color object
        [Browsable(false)]
        public string BearishCandleSerialize {
            get { return Serialize.BrushToString(BearishCandle); }
            set { BearishCandle = Serialize.StringToBrush(value); }
        }

        [XmlIgnore()]
        [Display(Name = "Color bullish candle", GroupName = "Color Parameters", Order = 5)]
        public Brush BullishCandle { get; set; }

        // Serialize our Color object
        [Browsable(false)]
        public string BullishCandleSerialize {
            get { return Serialize.BrushToString(BullishCandle); }
            set { BullishCandle = Serialize.StringToBrush(value); }
        }

        #endregion

    }
}

#region NinjaScript generated code. Neither change nor remove.

namespace NinjaTrader.NinjaScript.Indicators
{
	public partial class Indicator : NinjaTrader.Gui.NinjaScript.IndicatorRenderBase
	{
		private VolumePercentageNT8[] cacheVolumePercentageNT8;
		public VolumePercentageNT8 VolumePercentageNT8(int percentage, int tabTimes)
		{
			return VolumePercentageNT8(Input, percentage, tabTimes);
		}

		public VolumePercentageNT8 VolumePercentageNT8(ISeries<double> input, int percentage, int tabTimes)
		{
			if (cacheVolumePercentageNT8 != null)
				for (int idx = 0; idx < cacheVolumePercentageNT8.Length; idx++)
					if (cacheVolumePercentageNT8[idx] != null && cacheVolumePercentageNT8[idx].Percentage == percentage && cacheVolumePercentageNT8[idx].tabTimes == tabTimes && cacheVolumePercentageNT8[idx].EqualsInput(input))
						return cacheVolumePercentageNT8[idx];
			return CacheIndicator<VolumePercentageNT8>(new VolumePercentageNT8(){ Percentage = percentage, tabTimes = tabTimes }, input, ref cacheVolumePercentageNT8);
		}
	}
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
	public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
	{
		public Indicators.VolumePercentageNT8 VolumePercentageNT8(int percentage, int tabTimes)
		{
			return indicator.VolumePercentageNT8(Input, percentage, tabTimes);
		}

		public Indicators.VolumePercentageNT8 VolumePercentageNT8(ISeries<double> input , int percentage, int tabTimes)
		{
			return indicator.VolumePercentageNT8(input, percentage, tabTimes);
		}
	}
}

namespace NinjaTrader.NinjaScript.Strategies
{
	public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
	{
		public Indicators.VolumePercentageNT8 VolumePercentageNT8(int percentage, int tabTimes)
		{
			return indicator.VolumePercentageNT8(Input, percentage, tabTimes);
		}

		public Indicators.VolumePercentageNT8 VolumePercentageNT8(ISeries<double> input , int percentage, int tabTimes)
		{
			return indicator.VolumePercentageNT8(input, percentage, tabTimes);
		}
	}
}

#endregion
