###Hey all,
I have finished the policy tables, or instruction sets for each of the available, "walkable" locations.  

##Description of each file
obstacles.mat holds the states that are deemed obstacles in a 1-D array

targetStates.mat holds the states that are deemed walkable 1-D array

qMatrices.mat holds Q-Matrix in a cell container. Target state of each Q-Matrix is concurrent with the order of targetStates.mat

generatedPolicyTables.mat holds Policy Tables in a cell container. Target state of each Policy Table is concurrent with the order of targetStates.mat

finalPolicyTables.csv holds Policy Tables in a csv file. Ready to be parsed.  Value #'s 0-719 == 1st policy table, #'s 720-1439 == 2nd policy table, #'s 1440-2159 == 3rd policy table

targetStates.csv holds Policy Tables

##How to Use
generatedPolicyTables[i].getTargetState() == targetStates(i)
####Legend
1 == UP

2 == RIGHT

3 == DOWN

4 == LEFT


