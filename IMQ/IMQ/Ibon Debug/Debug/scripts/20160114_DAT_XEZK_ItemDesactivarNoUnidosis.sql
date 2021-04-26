update Item set Status = 2 where ID not in (select It.ID
												from Item It, [DrugUnidosisRelationship] rel, Item Uni
												where It.ID = rel.ParentItemID
												and Uni.ID = rel.ChildItemID
												and It.ItemType = 25 and Uni.ItemType = 17
											UNION
											select Uni.ID
												from Item It, [DrugUnidosisRelationship] rel, Item Uni
												where It.ID = rel.ParentItemID
												and Uni.ID = rel.ChildItemID
												and It.ItemType = 25 and Uni.ItemType = 17
											UNION
											select Uni.ID
												from Item Uni
												where Uni.ItemType <> 25)