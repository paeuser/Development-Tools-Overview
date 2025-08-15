def print_growing_triangle(n):
    for i in range(1, n + 1):
        for j in range(i):
            print('*', end=' ')
        print()

if __name__ == "__main__":
    print_growing_triangle(5)
