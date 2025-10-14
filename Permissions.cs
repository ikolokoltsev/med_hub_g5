[Flags]
public enum UserPermissions
{
  None = 0,
  CreateAdmin = 1,
  Give admin permissions = 2,
  Give AccountCreating = 4,
  Permission to view Permissions = 8,
  Permission to ad Location = 16,
  As Patient Read my own Journal = 32,

}



// As a user, I need to be able to log in.

// As a user, I need to be able to log out.

// As a user, I need to be able to request registration as a patient.

// As an admin with sufficient permissions, I need to be able to give admins the permission to handle the permission system, in fine granularity.

// As an admin with sufficient permissions, I need to be able to assign admins to certain regions.

// As an admin with sufficient permissions, I need to be able to give admins the permission to handle registrations.

// As an admin with sufficient permissions, I need to be able to give admins the permission to add locations.

// As an admin with sufficient permissions, I need to be able to give admins the permission to create accounts for personnel.

// As an admin with sufficient permissions, I need to be able to give admins the permission to view a list of who has permission to what.

// As an admin with sufficient permissions, I need to be able to add locations.

// As an admin with sufficient permissions, I need to be able to accept user registration as patients.

// As an admin with sufficient permissions, I need to be able to deny user registration as patients.

// As an admin with sufficient permissions, I need to be able to create accounts for personnel.

// As an admin with sufficient permissions, I need to be able to view a list of who has permission to what.

// As personnel with sufficient permissions, I need to be able to view a patients journal entries.

// As personnel with sufficient permissions, I need to be able to mark journal entries with different levels of read permissions.

// As personnel with sufficient permissions, I need to be able to register appointments.

// As personnel with sufficient permissions, I need to be able to modify appointments.

// As personnel with sufficient permissions, I need to be able to approve appointment requests.

// As personnel with sufficient permissions, I need to be able to view the schedule of a location.

// As a patient, I need to be able to view my own journal.

// As a patient, I need to be able to request an appointment.

// As a logged in user, I need to be able to view my schedule.
