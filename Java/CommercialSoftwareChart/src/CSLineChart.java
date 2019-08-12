
import java.awt.Color;
import java.text.DecimalFormat;
import org.jfree.chart.ChartFactory;
import org.jfree.chart.ChartPanel;
import org.jfree.chart.JFreeChart;
import org.jfree.chart.axis.NumberAxis;
import org.jfree.chart.plot.PlotOrientation;
import org.jfree.chart.plot.XYPlot;
import org.jfree.chart.renderer.xy.XYLineAndShapeRenderer;
import org.jfree.data.xy.XYDataset;
import org.jfree.data.xy.XYSeries;
import org.jfree.data.xy.XYSeriesCollection;
import org.jfree.ui.ApplicationFrame;

public class CSLineChart extends ApplicationFrame {

    /*Creates Demo */
	
    public CSLineChart(final String title) {

        super(title);

        final XYDataset dataset = createDataset();
        final JFreeChart chart = createChart(dataset);
        final ChartPanel chartPanel = new ChartPanel(chart);
        chartPanel.setPreferredSize(new java.awt.Dimension(500, 270));
        setContentPane(chartPanel);

    }
    
    /* Creates a sample dataset. */
    
    private XYDataset createDataset() {
        
        final XYSeries series1 = new XYSeries("Average Age of Taxfilers (Years) ");
        series1.add(2008, 47);
        series1.add(2009, 47);
        series1.add(2010, 47);
        series1.add(2011, 47);
        series1.add(2012, 48);
        

        final XYSeries series2 = new XYSeries("Average Age of Persons (Years) ");
        series2.add(2008, 39);
        series2.add(2009, 39);
        series2.add(2010, 39);
        series2.add(2011, 39);
        series2.add(2012, 39);
        
  
        
        final XYSeriesCollection dataset = new XYSeriesCollection();
        dataset.addSeries(series1);
        dataset.addSeries(series2);
       
                
        return dataset;
        
    }
    /* Creates a chart  */
    
    private JFreeChart createChart(final XYDataset dataset) {
        
        // create the chart...
        final JFreeChart chart = ChartFactory.createXYLineChart(
            "Average Age",      // chart title
            "Year",                      // x axis label
            "Age",                      // y axis label
            dataset,                  // data
            PlotOrientation.VERTICAL,
            true,                     // include legend
            true,                     // tooltips
            false                     // urls
        );

        // NOW DO SOME OPTIONAL CUSTOMISATION OF THE CHART...
        chart.setBackgroundPaint(Color.white);
        
        // get a reference to the plot for further customisation...
        final XYPlot plot = chart.getXYPlot();
        plot.setBackgroundPaint(Color.lightGray);
        plot.setDomainGridlinePaint(Color.white);
        plot.setRangeGridlinePaint(Color.white);
        
    
        plot.getRenderer().setSeriesPaint(0, Color.YELLOW);
        
        final XYLineAndShapeRenderer renderer = new XYLineAndShapeRenderer();
        
       renderer.setSeriesShapesVisible(3, false);
        plot.setRenderer(renderer);

        // change the auto tick unit selection to integer units only...
      
        final NumberAxis rangeAxis = (NumberAxis) plot.getRangeAxis();
        final DecimalFormat ft = new DecimalFormat("##.##");
        rangeAxis.setNumberFormatOverride(ft);
        
       final NumberAxis domainAxis = (NumberAxis) plot.getDomainAxis();
       final DecimalFormat format = new DecimalFormat("####");
       domainAxis.setNumberFormatOverride(format);
       
        
        return chart;
        
    }


}