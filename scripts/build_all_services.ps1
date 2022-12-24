cd ../src/services/catalog/
echo "Building Catalog Service Docker Image"
docker build -t iammukeshm/fluentpos-catalog-service:latest .
docker push iammukeshm/fluentpos-catalog-service:latest 

cd ../../../src/gateway/
echo "Building Gateway Docker Image" 
docker build -t iammukeshm/fluentpos-gateway:latest .
docker push iammukeshm/fluentpos-gateway:latest  