
import java.awt.Color;
import java.awt.Dimension;
import org.jfree.chart.ChartFactory;
import org.jfree.chart.ChartPanel;
import org.jfree.chart.JFreeChart;
import org.jfree.chart.axis.CategoryAxis;
import org.jfree.chart.axis.CategoryLabelPositions;
import org.jfree.chart.axis.NumberAxis;
import org.jfree.chart.plot.CategoryPlot;
import org.jfree.chart.plot.PlotOrientation;
import org.jfree.chart.renderer.category.BarRenderer;
import org.jfree.data.category.CategoryDataset;
import org.jfree.data.category.DefaultCategoryDataset;
import org.jfree.ui.ApplicationFrame;


public class CSBarChart extends ApplicationFrame {

    /*Creates a new demo instance.. */
	
    public CSBarChart(final String title) {

        super(title);

        final CategoryDataset dataset = createDataset();
        final JFreeChart chart = createChart(dataset);
        final ChartPanel chartPanel = new ChartPanel(chart);
        chartPanel.setPreferredSize(new Dimension(500, 270));
        setContentPane(chartPanel);

    }

    /*Returns a sample data set */
    private CategoryDataset createDataset() {
        
        // row keys...
        final String series1 = "Number of Taxfillers";
        final String series2 = "Number of Persons";
        final String series3 = "Number of Persons with total incomme";

        // column keys...
        final String category1 = "2008";
        final String category2 = "2009";
        final String category3 = "2010";
        final String category4 = "2011";
        final String category5 = "2012";
        
        // create the data set...
        final DefaultCategoryDataset dataset = new DefaultCategoryDataset();

        dataset.addValue(24986960, series1, category1);
        dataset.addValue(25244320, series1, category2);
        dataset.addValue(25484320, series1, category3);
        dataset.addValue(25869670, series1, category4);
        dataset.addValue(26155710, series1, category5);
       
        dataset.addValue(32124240, series2, category1);
        dataset.addValue(32425050, series2, category2);
        dataset.addValue(32700430, series2, category3);
        dataset.addValue(33087940, series2, category4);
        dataset.addValue(33399630, series2, category5);
        
        dataset.addValue(24731470, series3, category1);
        dataset.addValue(24964290, series3, category2);
        dataset.addValue(25227050, series3, category3);
        dataset.addValue(25599300, series3, category4);
        dataset.addValue(25797510, series3, category5);
     
        return dataset;
        
    }
    
    
    
    /* Creates a sample chart.*/
    
    private JFreeChart createChart(final CategoryDataset dataset) {
        
        // create the chart...
        final JFreeChart chart = ChartFactory.createBarChart(
            "Annual Neighbourhood Income and Demographics",         // chart title
            "YEAR",               // domain axis label
            "VALUE ($)",                  // range axis label
            dataset,                  // data
            PlotOrientation.VERTICAL, // orientation
            true,                     // include legend
            true,                     // tooltips?
            false                     // URLs?
        );

        // NOW DO SOME OPTIONAL CUSTOMIZATION OF THE CHART...

        // set the background color for the chart...
        chart.setBackgroundPaint(Color.pink);
        

        // get a reference to the plot for further customization...
        final CategoryPlot plot = chart.getCategoryPlot();
        plot.setBackgroundPaint(Color.lightGray);
        plot.setDomainGridlinePaint(Color.white);
        plot.setRangeGridlinePaint(Color.white);
        
        // set the range axis to display integers only...
        final NumberAxis rangeAxis = (NumberAxis) plot.getRangeAxis();
        rangeAxis.setStandardTickUnits(NumberAxis.createIntegerTickUnits());

        // enable bar outlines...
        final BarRenderer renderer = (BarRenderer) plot.getRenderer();
        renderer.setDrawBarOutline(true);
        
        // set up color for series
        renderer.setSeriesPaint(0, Color.CYAN);
        renderer.setSeriesPaint(1, Color.ORANGE);
        renderer.setSeriesPaint(2, Color.MAGENTA);
        
        //have no gap between bars
        renderer.setItemMargin(0);

        final CategoryAxis domainAxis = plot.getDomainAxis();
        domainAxis.setCategoryLabelPositions(
            CategoryLabelPositions.createUpRotationLabelPositions(Math.PI / 6.0)
        );
       
        return chart;
        
    }
}