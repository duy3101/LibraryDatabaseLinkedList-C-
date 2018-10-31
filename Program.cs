//NGOC DEWEY NGUYEN, BIT143, FALL 2017, ASSIGMENT 3, VER 1.1 (revised)
//
//Fixed: sort node alphabetically when adding and removing node.
//       remove an author node when theres no book in that author.



//When I was implementing add and remove, the most important thing to me was 
//to figure out the relationhip between author and book linked list.
//every author LL have a book LL inside them. 
//Access the book LL and retrieve datas were the foundation part of this assigment
//At first,I did not care about running time at all. It was more like getting the logic down. 
//Hence why, there were multiple loops running in add and remove
//Later, I merged all the loops into a single loop so the program can be easier to trace and faster.
//The sorting algorithm was more difficult and I had to search online for tips.
//I tend to make a lot of methods doing its own things, then I merge them together for more
//efficient usage. 

//If this assignment was a real life professional work, if I were to be stuck on something, I'd ask my 
//coworkers for advices. If there is a required due date, then I would plan out my schedule 
//and finish the project by parts. Then I would ask my coworkers to trace and debug the program
//with me.






using System;
using System.Collections.Generic;
using System.Text;

namespace MulitList_Starter
{
    class Program
    {
        static void Main(string[] args)
        {
            (new UserInterface()).RunProgram();

            // Or, you could go with the more traditional:
            // UserInterface ui = new UserInterface();
            // ui.RunProgram();
        }
    }

    // Bit of a hack, but still an interesting idea....
    enum MenuOptions
    {
        // DO NOT USE ZERO!
        // (TryParse will set choice to zero if a non-number string is typed,
        // and we don't want to accidentally set nChoice to be a member of this enum!)
        QUIT = 1,
        ADD_BOOK,
        PRINT,
        REMOVE_BOOK,
        RUN_TESTS
    }

    class UserInterface
    {
        MultiLinkedListOfBooks theList;



        public void RunProgram()
        {
            int nChoice;
            theList = new MultiLinkedListOfBooks();

            do // main loop
            {
                Console.WriteLine("Your options:");
                Console.WriteLine("{0} : End the program", (int)MenuOptions.QUIT);
                Console.WriteLine("{0} : Add a book", (int)MenuOptions.ADD_BOOK);
                Console.WriteLine("{0} : Print all books", (int)MenuOptions.PRINT);
                Console.WriteLine("{0} : Remove a Book", (int)MenuOptions.REMOVE_BOOK);
                Console.WriteLine("{0} : RUN TESTS", (int)MenuOptions.RUN_TESTS);
                if (!Int32.TryParse(Console.ReadLine(), out nChoice))
                {
                    Console.WriteLine("You need to type in a valid, whole number!");
                    continue;
                }
                switch ((MenuOptions)nChoice)
                {
                    case MenuOptions.QUIT:
                        Console.WriteLine("Thank you for using the multi-list program!");
                        break;
                    case MenuOptions.ADD_BOOK:
                        this.AddBook();
                        break;
                    case MenuOptions.PRINT:
                        theList.Print();
                        break;
                    case MenuOptions.REMOVE_BOOK:
                        this.RemoveBook();
                        break;
                    case MenuOptions.RUN_TESTS:
                        //AllTests tester = new AllTests(); //I dont know what is this for so I commented out.
                        //tester.RunTests();
                        break;
                    default:
                        Console.WriteLine("I'm sorry, but that wasn't a valid menu option");
                        break;

                }
            } while (nChoice != (int)MenuOptions.QUIT);
        }

        public void AddBook()
        {

			

            Console.WriteLine("ADD A BOOK!");

            Console.WriteLine("Author name?");
            string author = Console.ReadLine();

            Console.WriteLine("Title?");
            string title = Console.ReadLine();

            double price = -1;
            while (price < 0)
            {
                Console.WriteLine("Price?");
                if (!Double.TryParse(Console.ReadLine(), out price))
                {
                    Console.WriteLine("I'm sorry, but that's not a number!");
                    price = -1;
                }
                else if (price < 0)
                {
                    Console.WriteLine("I'm sorry, but the number must be zero, or greater!!");
                }
            }

            ErrorCode ec = theList.Add(author, title, price);

			// STUDENTS: YOUR ERROR-CHECKING CODE SHOULD GO HERE!

			if (ec == ErrorCode.OK)
			{
				Console.WriteLine("Book added!");
			}
            if (ec == ErrorCode.DuplicateBook)
			{
				Console.WriteLine("Cannot add duplicate!");
			}

        }

        public void RemoveBook()
        {
            Console.WriteLine("REMOVE A BOOK!");

            Console.WriteLine("Author name?");
            string author = Console.ReadLine();

            Console.WriteLine("Title?");
            string title = Console.ReadLine();

            ErrorCode ec = theList.Remove(author, title);

            // STUDENTS: YOUR ERROR-CHECKING CODE SHOULD GO HERE!

            if (ec == ErrorCode.OK) {
                Console.WriteLine("Book removed!");
            }
            if (ec == ErrorCode.BookNotFound) {
                Console.WriteLine("Book cannot be found!");
            }


        }
    }

    enum ErrorCode
    {
        OK,
        DuplicateBook,
        BookNotFound
    }

    class MultiLinkedListOfBooks
    {
        AuthorLinkedListNode authorList = new AuthorLinkedListNode();




        private class AuthorLinkedListNode
        {

            public Book collection = new Book();
            public String author;
            public AuthorLinkedListNode next;



            public AuthorLinkedListNode(String name)
            {
                author = name;
            }

			public AuthorLinkedListNode()
			{
				
			}


			


			protected AuthorLinkedListNode front;




            public int Compare(string author, string otherAuthorsName)
            {
                int result = string.Compare(author, otherAuthorsName);
                return result;
                //if result = -1 author is before other
                //if result = 1 author is after other
                //if result = 0 they are equal
            }



            public AuthorLinkedListNode AddSort(String name)
            {

                AuthorLinkedListNode current = front;
                AuthorLinkedListNode beforecurrent = front;
                AuthorLinkedListNode newNode = new AuthorLinkedListNode(name);

                if (front == null)
                {
                    front = new AuthorLinkedListNode(name);
                    return front;

                }



                else
                {

                    while (current.next != null && Compare(current.author, name) <= 0)
                    {
                        beforecurrent = current;
                        current = current.next;

                    }


                    if (current == front && Compare(current.author, name) >= 0)
                    {
                        newNode.next = front;
                        front = newNode;
                        return newNode;
                    }


                    if (Compare(current.author, name) >= 0)
                    {

                        beforecurrent.next = newNode;
                        newNode.next = current;

                        return newNode;
                    }


                    newNode.next = current.next;
                    current.next = newNode;
                    return newNode;


                }

            }

            public AuthorLinkedListNode FindAuthorNode(String name)
            {


                AuthorLinkedListNode current = front;
                while (current != null)
                {


                    String currentauthor = current.author;
                    if (currentauthor.Equals(name))
                    {
                        return current;
                    }

                    current = current.next;

                }
                return null;
            }




            public bool FindBookInAuthor(AuthorLinkedListNode authornode, String title)
            {


                Book authorcollection = authornode.collection;
                if (authorcollection.Find(title))
                {
                    return true;

                }
                return false;
            }



            public ErrorCode AddBookToAuthor(AuthorLinkedListNode authornode, String title, double price)
            {


                Book authorcollection = authornode.collection;
                return authorcollection.AddSort(title, price);


            }


            public void RemoveBookFromAuthor(AuthorLinkedListNode authornode, String title)
            {

                Book authorcollection = authornode.collection;
                authorcollection.RemoveAt(title);
            }






            public void RemoveAt(String author)
            {


                AuthorLinkedListNode current = front;
                AuthorLinkedListNode beforecurrent = front;


                while (current.next != null)
                {


                    String currentauthor = current.author;

                    if (currentauthor.Equals(author))
                    {
                        break;
                    }


                    beforecurrent = current;
                    current = current.next;

                }

                if (current == front)
                {
                    front = front.next;
                }


                beforecurrent.next = current.next;



            }

            public void print()
            {

                AuthorLinkedListNode current = front;
                while (current != null)
                {
                    Console.WriteLine();
                    Console.WriteLine("Author: " + current.author);
                    Book currentcollection = current.collection;
                    currentcollection.Print();
                    current = current.next;
                }


            }




        }
        public class Book
        {

			public class LinkedListNode
			{

				private String title;
				private double price;
				private LinkedListNode next;


				public void SetAuthor(String title)
				{

					this.title = title;
				}

				public String GetTitle()
				{
					return title;
				}

				public void SetNext(LinkedListNode next)
				{
					this.next = next;
				}

				public LinkedListNode GetNext()
				{
					return next;
				}

				public double GetPrice()
				{
					return price;
				}

				public LinkedListNode(String title, double price)
				{

					this.title = title;
					this.price = price;
				}

			}


            protected LinkedListNode front;


            public LinkedListNode ReturnFront() 
            {

                return front;
            }


			

			public int Compare(string title, string otherBooksTitle)
            {
                int result = string.Compare(title, otherBooksTitle);
                return result;

            }



            public ErrorCode AddSort(String title, double price)
			{

                LinkedListNode current = front;
                LinkedListNode beforecurrent = front;
				LinkedListNode newNode = new LinkedListNode(title, price);

				if (front == null)
				{
                    front = new LinkedListNode(title, price);
                    return ErrorCode.OK;

				}



				else
				{

                    while (current.GetNext() != null && Compare(current.GetTitle(), title) < 0)
					{
						beforecurrent = current;
						current = current.GetNext();
						
					}


                    if (current == front && Compare(current.GetTitle(), title) > 0)
					{
                        newNode.SetNext(front);
                        front = newNode;
                        return ErrorCode.OK;
						
					}


                    if (Compare(current.GetTitle(), title) > 0)
					{


                        beforecurrent.SetNext(newNode);
                        newNode.SetNext(current);
                        return ErrorCode.OK;
						
					}

                    if (Compare(current.GetTitle(), title) == 0) 
                    {
                        return ErrorCode.DuplicateBook;
                    }
					
				

                    newNode.SetNext(current.GetNext());
                    current.SetNext(newNode);
                    return ErrorCode.OK;
                   

				}


			}


			public bool Find(String title)
			{
				if (front == null)
				{
					return false;
				}

				LinkedListNode current = front;
				while (current != null)
				{


					String currenttitle = current.GetTitle();
					if (currenttitle.Equals(title))
					{
						return true;
					}

					current = current.GetNext();




				}
				return false;

			}

            public void RemoveAt(String title) {

                			
				LinkedListNode current = front;
                LinkedListNode beforecurrent = front;


                while (current != null) {
                    

					String currenttitle = current.GetTitle();

					if (currenttitle.Equals(title))
					{
						break;
					}

                    beforecurrent = current;
					current = current.GetNext();
					
                }


				if (current == front)
				{
                    front = front.GetNext();
				}



                beforecurrent.SetNext(current.GetNext());

			}



			// Print out the book info (title, price).
			// Indenting this would be nice
			public void Print()
            {
                LinkedListNode current = front;
                while (current != null) {

                    Console.WriteLine("Title: " + current.GetTitle());
					Console.WriteLine("Price: $" + current.GetPrice());
                    current = current.GetNext();
                }
                Console.WriteLine();
            }
        }

        public ErrorCode Add(string author, string title, double price)
        {
            

            AuthorLinkedListNode authornode = authorList.FindAuthorNode(author);



            if(authornode == null) {
           			


				authornode = authorList.AddSort(author);
                return authorList.AddBookToAuthor(authornode, title, price);
				
            }


            return authorList.AddBookToAuthor(authornode, title, price);



           
        }

        public void Print()
        {
            authorList.print();
        }

        public ErrorCode Remove(string author, string title)
        {
            



            AuthorLinkedListNode authornode = authorList.FindAuthorNode(author);



            if (authornode == null)
            {


                return ErrorCode.BookNotFound;
            }


            else {

                if (authorList.FindBookInAuthor(authornode, title)) 
                {
                    authorList.RemoveBookFromAuthor(authornode, title);



                    Book.LinkedListNode front = authornode.collection.ReturnFront();

                    if (front == null) //if an author node have no book node
					{
                        
                        authorList.RemoveAt(author);


                    }


                    return ErrorCode.OK;
                }
				

                return ErrorCode.BookNotFound;

                    
            }
        }
    }
}
