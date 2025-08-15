def print_growing_triangle(n):
    # Outer loop: iterate from 1 to n (inclusive)
    # Controls the number of rows to print
    for i in range(1, n + 1):
        # Inner loop: iterate i times for the current row
        # Prints the correct number of stars for that row
        for j in range(i):
            # Print a star followed by a space
            # end=' ' prevents print() from moving to a new line,
            # so stars are printed on the same line separated by spaces
            print('*', end=' ')
        # After printing all stars in the current row,
        # print() without arguments moves to the next line
        print()

if __name__ == "__main__":
    # Call the function to print a growing triangle of height 5
    print_growing_triangle(5)
