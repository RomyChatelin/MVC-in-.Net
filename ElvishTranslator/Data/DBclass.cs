using ElvishTranslator.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElvishTranslator.Data
{

    //db class and db helper class 
    //FIXME: seperate db connect and queries 
    public class DBclass
    {

        //Gets connection and all pets
        public List<Pet> getPets()
        {

            var pets = new List<Pet>(); 
            using (var connection = new MySqlConnection("server = localhost; user = root; database = groepsopdracht_rentapet ; port = 3306; password = ''"))
            { 

                connection.Open(); 
                using(var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT pets.id_pet, pets.name, animals.animal, breeds.breed FROM pets, animals, breeds WHERE pets.id_breed = breeds.id_breed AND breeds.id_animal = animals.id_animal";

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                          
                             //Put results into List of objects! 
                             int pet_id = reader.GetInt32(reader.GetOrdinal("id_pet")); 
                             string name = reader.GetString(reader.GetOrdinal("name"));
                             string animal = reader.GetString(reader.GetOrdinal("animal"));
                             string breed = reader.GetString(reader.GetOrdinal("breed"));

                           
                            pets.Add(new Pet() { pet_id = pet_id, name = name, animal = animal, breed = breed });
                            

                        }
                    }
                    
                }
            }
            //Returns pets as a list
            return pets;
        }

    }
}
