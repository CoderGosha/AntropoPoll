docker run --name antropopollsql --network=127.0.0.1 -e POSTGRES_PASSWORD=1q2w3e4r -d -p 5433:5432 -v $HOME/docker/volumes/postgres:/var/lib/postgresql/data  postgres;
docker exec -it antropopollsql psql -U postgres -c "CREATE DATABASE antropopoll";


sudo docker build -t antropopoll .  <br />
sudo docker run -d --rm  -p 5000:80 --name antropopoll antropopoll  <br />

