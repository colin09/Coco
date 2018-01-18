import (
	"io"
	"net/http"
	"os/exec"
	"log"
)

func reLunch(){
	cmd := exec.Command("sh","./deploy.sh")
	err := cmd.Start()
	if err != nil{
		log.Fatal(err)
	}
	err = cmd.Wait()
}

func pageHandle(w http.ResponseWrite,r *http.Request){
	io.WriteString(w,"start to deploy ......")
	reLunch()
}

func main(){
	http.HandleFunc("/",pageHandle)
	http.ListenAndServe(":9000",nil)
}