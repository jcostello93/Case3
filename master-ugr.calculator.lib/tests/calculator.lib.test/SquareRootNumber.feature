Feature: Square Root Number

Scenario: Get the square root of a positive number
    Given the number 16
    When I calculate the square root
    Then the square root should be 4

Scenario: Get the square root of zero
    Given the number 0
    When I calculate the square root
    Then the square root should be 0

Scenario: Get the square root of a negative number
    Given the number -9
    When I calculate the square root
    Then the square root should be NaN