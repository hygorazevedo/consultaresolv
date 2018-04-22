namespace ConsultaResolv
{
    public class Resources
    {
        #region File PHP
        public static string parte1 = @"<?php
ini_set('soap.wsdl_cache_enabled', 0);
ini_set('soap.wsdl_cache_ttl', 0);
ini_set('soap.wsdl_cache', 0);
ini_set('default_socket_timeout', 600);
ini_set('memory_limit', '2048M');
set_time_limit(0);
$context = stream_context_create(array(
    'ssl' => array(
        'verify_peer' => false,
        'verify_peer_name' => false,
        'allow_self_signed' => true,
    ),
));

$host = 'poc.scoresolv.com.br';

$authWSDL = 'http://' . $host . '/Service/auth/wsdl';
$searchWSDL = 'http://' . $host . '/Service/search/wsdl';
$token = 'abec8df7-afa2046993919bb2-93919bb2635c32e3-635c32e3';
$tipoResposta = 'xml';

 
$tipoConsulta = 'consulta_situacao_cpf_ole';
$dadosConsulta = " + '"' + "<?xml version='1.0' encoding='UTF-8'?> <consulta> <dados>";
        
        public static string consulta = @"
<cpf>{0}</cpf>                   <data_nasc >{1}</data_nasc>" +
@"
</dados> </consulta> " + '"'+ ';';

        public static string parte2 = @"
$clientSearch = new SoapClient($searchWSDL, array('stream_context' => $context));
$response = $clientSearch->searchByDados($token, $dadosConsulta, $tipoConsulta, $tipoResposta);
if ($tipoResposta == 'xml') {
    header('Content-type: text/xml; charset=utf-8');
} else {
    echo '<pre>';
}
print_r($response);";
        #endregion

        public static string query = @"select top (60) cpf, convert(varchar(10),datanascimento,126) datanascimento from consulta";
    }
}
