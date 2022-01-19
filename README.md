
# COVID-19 Spread Simulator

This is a COVID-19 pandemic simulator. through a computer simulation. Most of us have had or know someone who has had contact with a positive case of COVID-19 during the past couple of years. That is why I wanted to create a simulation and see how the spread of the infection can be represented in a computer.

<img src="https://i.ibb.co/x1LKMfc/Main-Screenshot.jpg">

## Running the simulation

To run the simulation you just Unity3D engine on your machine. Then checkout this repo and run the *SampleScene* scene on Unity3D software.

## Configuring the simulation

This simulation is based on probabilities. You can chance the following values of the simulation to generate different scenarions:

- Number of subjects.
- Level of initial infection.
- Level of subject complaince (how many will follow medical rules).
- Probability of infection upon collision.

## Simulation Areas

There are 4 main areas on this simulation:

- The HOME area. Which is where subjects are created and also where any infected subject is going to spend some time doing the quarantine.
- The PARK area. This is an open space where collisions between infected subjects are less likely to occur. It is also the place recommended by authorities so subjects with a high level of rule compliance are going to be frequent.
- The DANGER area. This is an enclosed space with lots of barriers which lead to high ratios of collision. Also, the authorities recommend to avoid these closed spaces so subjects with low level of rule compliance are going to be frequent there.
- Finally I created the HOSPITAL area, which is a space for infected subjects whose health level has decreased a lot due to the infection.

## Subject Appearance

To visually represent the state of any subject on the simulation, there are four main colours on them:

- A GREEN subject is one that is not wearing a mask and is not vaccinated.
- A LIGHT BLUE subject is one that either wears a mask or is vaccinated.
- A DARK BLUE subject is one that is wearing a mask and also have been fully vaccinated.
- A RED subject is currently infected.

<img src="https://i.ibb.co/M6bmrRk/Main-Subject-Colours.jpg" border="0">


## Simulation Results

The simulation can be previewed freely on the open world or via Display1 camera. If Display1 is selected, then the results of the current simulation are going to be shown on the top-right area of the UI.

<img src="https://i.ibb.co/TBRsjYS/Main-Subject-Results.jpg" alt="Main-Subject-Results">

## Disclaimer

Any results presented on this project are <b>fictional</b> and the data is the result of a computer simulation. Official sites must be consulted to get real insights on the COVID-19 spread.