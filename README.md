

# Group project short notes

NB: dev branch created and pushed.


User.cs
- User able to request to registration and register (as a patient);
- User able to log in;
- User able to log out;


SuperAdmin.cs
- Give to other admins permission to handle requests
- Assign admins to certain regions
- Give admins permission to handle registrations - in terms of accept or deny
- Give admins permission to handle locations
- Give admins permission to create account for personnels
- Give admins the permission to view a list of who has permission to what.
- Add locations
- Accept user registration as patient
- Deny user registration as patient
- Create accounts for personnels
- View a list of who has permission to what
- View a patients journal entries
- Mark journal entries with different levels of read permissions
- Register appointments
- Modify appointments
- Approve appointment requests
- View the schedule of a location


Patient.cs
- View my own journal
- Request an appointment

Logged in users - view own schedules


Classes:
    - Program.cs
            - 
    - Particitant.cs
            - 
    - Menu.cs
            - None,
            - 
    - User.cs
            - SufficientAdmin,
            - Personnel,
            - Patient,



# Step-by-step code writing approach

1. Define users permission;
    - SuperAdmin like IT support ()
    - OtherAdmins like Doctors, Nurses, Receptionists ()
    - Patient ()


