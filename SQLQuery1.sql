  SELECT Owner.Id, Owner.Email, Owner.Name, Owner.Address, Owner.NeighborhoodId, Owner.Phone, Neighborhood.Name as Neighborhood
                                        FROM Owner
                                        JOIN Neighborhood on Neighborhood.Id = NeighborhoodId