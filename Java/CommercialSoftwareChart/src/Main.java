import org.jfree.ui.RefineryUtilities;


public class Main {

	 public static void main(final String[] args) {

		    final CSBarChart bData = new CSBarChart("Annual Neighbourhood Income and Demographics");
		     bData.pack();
		    RefineryUtilities.centerFrameOnScreen(bData);
		    bData.setVisible(true);
		    
		    final CSLineChart demo = new CSLineChart("Average Age of Taxfilers");
	        demo.pack();
	        RefineryUtilities.centerFrameOnScreen(demo);
	        demo.setVisible(true);

	    }
}
