package ru.geekbrains.lesson03;

import java.util.Arrays;

public class HomeWorkApp {
    public static void main(String[] args) {
        task1(new int[]{1, 1, 0, 0, 1, 0, 1, 1, 0, 0});
        task2();
        task3(new int[]{1, 5, 3, 2, 11, 4, 5, 2, 4, 8, 9, 1});
    }

    /**
     * Task 1
     * Set an integer array consisting of elements 0 and 1.
     * For example: [ 1, 1, 0, 0, 1, 0, 1, 1, 0, 0 ]. Use the loop and condition to replace 0 with 1, 1 with 0;
     *
     * @param array - your source arra
     */
    public static void task1(int[] array) {
        System.out.println("Task1:");
        printCurrentArray(array);
        for (int i = 0; i < array.length; i++) {
            array[i] = (array[i] == 1) ? 0 : (array[i] == 0) ? 1 : array[i];
        }
        printNewArray(array);
    }

    /**
     * Task 2
     * Set an empty integer array of length 100. Use a loop to fill it with values 1 2 3 4 5 6 7 8 ... 100;
     */
    public static void task2() {
        System.out.println("\nTask2:");
        int[] array = new int[100];
        for (int i = 0; i < array.length; i++) {
            array[i] = i + 1;
        }
        printNewArray(array);
    }

    /**
     * Task 3
     * Set an array [ 1, 5, 3, 2, 11, 4, 5, 2, 4, 8, 9, 1 ] go through it in a loop,
     * and multiply the numbers less than 6 by 2;
     *
     * @param array to process
     */
    public static void task3(int[] array) {
        System.out.println("\nTask3:");
        printCurrentArray(array);
        for (int i = 0; i < array.length; i++) {
            if (array[i] < 6) {
                array[i] *= 2;
            }
        }
        printNewArray(array);
    }

    /**
     * Additional method to print existing array
     *
     * @param array to print
     */
    public static void printCurrentArray(int[] array) {
        System.out.println("Your array:");
        System.out.println(Arrays.toString(array));
    }

    /**
     * Additional method to print new array
     *
     * @param array to print
     */
    public static void printNewArray(int[] array) {
        System.out.println("New array:");
        System.out.println(Arrays.toString(array));
    }
}