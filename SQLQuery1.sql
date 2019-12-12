select * from Users u
inner join OrganizationalProfiles op on u.ProfileId = op.Id 
inner join Documents d on d.OrganizationalProfileId = op.Id 
