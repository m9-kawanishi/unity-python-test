import socket, time

def start_server():
    host = 'localhost'
    port = 5005
    with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
        s.bind((host, port))
        s.listen(1)
        print("Waiting for connection...")
        conn, addr = s.accept()
        with conn:
            print(f"Connected by {addr}")
            while True:
                data = conn.recv(1024)
                if not data:
                    break
                print("Collision Detected:", data.decode())
                # ここでTrue/Falseに基づいて必要な処理を行う
                print("接触したことを確認しました！！！！！！！！")
                print("")
                print("Objects have made contact with each other.")
                time.sleep(10)

if __name__ == '__main__':
    start_server()
