using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndreTurismoApp.Models;

namespace AndreTurismoApp.Services
{
    public class HotelServices
    {
        readonly string strconn = @"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;AttachDbFileName=C:\dev-aula\AgenciaTurismo\AgenciaTurismo\docs\Scripts\AgenciaTurismo.mdf";
        readonly SqlConnection Conn;

        public HotelServices()
        {
            Conn = new SqlConnection(strconn);
            Conn.Open();
        }

        public bool Insert(Hotel hotel)
        {
            bool status = false;

            try
            {
                string strInsert = "insert into Hotel (Name, IdAddress, Price) " +
                    "values (@Name, @IdAddress, @Price)";

                SqlCommand commandInsert = new SqlCommand(strInsert, Conn);

                commandInsert.Parameters.Add(new SqlParameter("@Name", hotel.Name));
                commandInsert.Parameters.Add(new SqlParameter("@IdAddress", InsertAddress(hotel)));
                commandInsert.Parameters.Add(new SqlParameter("@Price", hotel.Price));

                commandInsert.ExecuteNonQuery();
                status = true;
            }
            catch
            {
                status = false;
                throw;
            }
            finally
            {
                Conn.Close();
            }
            return status;
        }

        private int InsertAddress(Hotel hotel)
        {
            string strInsert = "insert into Endereco (Logradouro, Numero, Bairro, CEP, Complemento, IdCidade) " +
            "values (@Logradouro, @Numero, @Bairro, @CEP, @Complemento, @IdCidade); select cast(scope_identity() as int)";

            SqlCommand commandInsert = new SqlCommand(strInsert, Conn);

            commandInsert.Parameters.Add(new SqlParameter("@Logradouro", hotel.address.Street));
            commandInsert.Parameters.Add(new SqlParameter("@Numero", hotel.address.Number));
            commandInsert.Parameters.Add(new SqlParameter("@Bairro", hotel.address.District));
            commandInsert.Parameters.Add(new SqlParameter("@CEP", hotel.address.ZipCode));
            commandInsert.Parameters.Add(new SqlParameter("@Complemento", hotel.address.Complement));
            commandInsert.Parameters.Add(new SqlParameter("@IdCidade", InsertCity(hotel.address)));

            return (int)commandInsert.ExecuteScalar();
        }

        private int InsertCity(Address address)
        {
            string strInsert = "insert into Cidade (Descricao) values (@Descricao); select cast(scope_identity() as int)";
            SqlCommand commandInsert = new SqlCommand(strInsert, Conn);
            commandInsert.Parameters.Add(new SqlParameter("@Descricao", address.city.Description));

            return (int)commandInsert.ExecuteScalar();
        }

        //public List<Hotel> FindAll()
        //{
        //    List<Hotel> hotellist = new();
        //    StringBuilder sb = new StringBuilder();

        //    sb.Append("select h.Id, h.Name, h.IdAddress, h.Price, h.RegisterDate, e.Id, e.Logradouro," + 
        //        " e.Numero, e.Bairro, e.CEP, e.Complemento,e.DtCadastro ," +
        //        " cid.Id, cid.Descricao, cid.DtCadastro as data" +
        //        " from Hotel as h" +
        //        " Join Endereco e ON h.IdAddress = e.Id" +
        //        " Join Cidade as cid ON cid.Id = e.IdCidade");

        //    SqlCommand commandSelect = new(sb.ToString(), Conn);
        //    SqlDataReader dr = commandSelect.ExecuteReader();

        //    while (dr.Read())
        //    {
        //        Hotel hotel = new();

        //        hotel.Id = (int)dr["Id"];
        //        hotel.Name = (string)dr["Name"];
        //        hotel.address = new Address()
        //        {
        //            Id = (int)dr["Id"],
        //            Street = (string)dr["Logradouro"],
        //            Number = (int)dr["Numero"],
        //            District = (string)dr["Bairro"],
        //            ZipCode = (string)dr["CEP"],
        //            Complement = (string)dr["Complemento"],
        //            city = new City()
        //            {
        //                Id = (int)dr["Id"],
        //                Description = (string)dr["Descricao"],
        //                RegisterDate = (DateTime)dr["data"]
        //            },
        //            RegisterDate = (DateTime)dr["Dtcadastro"]
        //        };
        //        hotel.RegisterDate = (DateTime)dr["RegisterDate"];
        //        hotel.Price = (decimal)dr["Price"];

        //        hotellist.Add(hotel);
        //    }
        //    return hotellist;
        //}

        public bool Update(int id, string name)
        {
            bool status = false;

            try
            {
                string strUpdate = "update Hotel Set Name = " + "'" + name + "' where Id = " + id;

                SqlCommand commandUpdate = new SqlCommand(strUpdate, Conn);

                commandUpdate.ExecuteNonQuery();
                status = true;
            }
            catch
            {
                status = false;
                throw;
            }
            finally
            {
                Conn.Close();
            }
            return status;
        }

        public bool Delete(int id)
        {
            bool status = false;

            try
            {
                string strDelete = $"Delete from Hotel where Id = {id}";

                SqlCommand commandDelete = new SqlCommand(strDelete, Conn);

                commandDelete.ExecuteNonQuery();
                status = true;
            }
            catch
            {
                status = false;
                throw;
            }
            finally
            {
                Conn.Close();
            }
            return status;
        }
    }
}
