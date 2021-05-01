package ru.geekbrains.lesson03;

import java.util.Arrays;

public class HomeWorkApp {
    public static void main(String[] args) {
        task1(new int[]{1, 1, 0, 0, 1, 0, 1, 1, 0, 0});
    }

    /**
     * Task 1
     * Set an integer array consisting of elements 0 and 1.
     * For example: [ 1, 1, 0, 0, 1, 0, 1, 1, 0, 0 ]. Use the loop and condition to replace 0 with 1, 1 with 0;
     * @param array - your source arra
     */
    public static void task1(int[] array) {
        System.out.println("Task1:");
        System.out.println("Your array:");
        System.out.println(Arrays.toString(array));
        for (int i = 0; i < array.length; i++) {
            array[i] = (array[i] == 1) ? 0 : (array[i] == 0) ? 1 : array[i];
        }
        System.out.println("New Array:");
        System.out.println(Arrays.toString(array));

    }
}