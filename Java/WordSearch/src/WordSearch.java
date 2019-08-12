import javax.swing.*;
import java.util.ArrayList;

// Application that allows the user to input a word to search, along with as many sentences as they want
// And then determines how many times the search word appears in their sentences
// Exit by entering "quit" as a sentence
class Wordsearch{
	// in order for that to happen we need to be able to hold a list of their sentences, and remember the word
	// they want to search.
	// index keeps tab on the number of sentences 
	// track keeps tab 
	private String word = "" ;
	private ArrayList<String> sentences;
	private String[] sentence;
	private int index = 0;
	private int track = 0;
	
	
	// deals with user input
	
	public void getUserInput(){
		sentences= new ArrayList<String>();
		sentence = new String[sentences.size()];
		
		word = JOptionPane.showInputDialog("Enter a search word");
		if(word == null || word.equals(""))
			System.exit(0);
		
		sentences.add(JOptionPane.showInputDialog("Enter a sentence or quit: "));

		
		while(!(sentences.get(index).equals("quit"))){
			 index++;
			 sentences.add(JOptionPane.showInputDialog("Enter a sentence: "));
	}
	}
	
	// returns the number of times the search word appears 

	public int wordcount(){
		for (int i =0; i < sentences.size(); i++)
			if (sentences.get(i).contains(word))
				this.track++;
		
		return track;
	}
	
	// output of the object

	public String toString(){
		String s="";
		s +="Search Word: " + word + "\n";
		for (int i=0;i<sentences.size();i++)
			s += sentences.get(i) + "\n";
		
		s += "Hits: " + this.wordcount();
		s +="\nNumer of Sentences: " + this.numberOfSentences();
		
		return s;
	}
	
	public int numberOfSentences(){
		return sentences.size();
	}
	
	public String [] getSentences(){
		sentence=sentences.toArray(new String[sentences.size()]);
		
		return sentence;
	}
	
	public String getSearchWord(){
		return word;
	}
	
}
