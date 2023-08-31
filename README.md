# Automata Celular para la simulacion del crecimiento de un tumor en 3D
Autor: Carlos Carret MIranda @Krtucho
## ¿Qué es el cáncer?
- Es una enfermedad que se caracteriza por el crecimiento descontrolado de células anormales en el cuerpo.
- Estas células pueden formar una masa llamada tumor.
- No todos los tumores son cancerosos; los tumores benignos no se propagan a otras partes del cuerpo y no son una amenaza para la vida.
- Sin embargo, los tumores malignos pueden invadir los tejidos cercanos y diseminarse a otras partes del cuerpo a través del sistema sanguíneo y linfático

## ¿Qué herramienta nos propusimos desarrollar para ayudar a la lucha contra el cáncer?
Simulación del crecimiento de un tumor en órganos del cuerpo humano, surgiendo en el tejido epitelial, conocidos como carcinomas.

## Autómata Celular
- Un autómata celular es un modelo matemático para un sistema dinámico que evoluciona en pasos discretos. 
- Consiste en una cuadrícula regular de celdas, cada una en uno de un número finito de estados. 
- Cada celda se actualiza en función de un conjunto de reglas fijas que determinan su nuevo estado en términos del estado actual de la celda y los estados de las celdas vecinas. 
- Los autómatas celulares son utilizados para modelar sistemas naturales que pueden ser descritos como una colección masiva de objetos simples que interactúan localmente unos con otros. 
- Son utilizados en diversas áreas como la física, la biología teórica y el modelado de microestructuras.

## Beneficioes del uso de un autómata celular para simular el crecimiento de un tumor en pacientes con cáncer:

- Modelado del crecimiento tumoral: Un autómata celular puede ser utilizado para simular la expansión y proliferación de células cancerosas en un entorno virtual. Cada célula en el autómata celular representa una célula cancerosa y se actualiza en función de reglas específicas que determinan su comportamiento y crecimiento. Esto puede ayudar a comprender mejor cómo se desarrolla y se propaga el tumor en diferentes escenarios.
 - Evaluación de tratamientos: Al simular el crecimiento del tumor con un autómata celular, es posible evaluar diferentes estrategias de tratamiento y su efectividad en la reducción o control del tumor. Por ejemplo, se pueden simular diferentes terapias, como la quimioterapia o la radioterapia, y observar cómo afectan el crecimiento y la propagación del tumor en el modelo.
 - Optimización de terapias personalizadas: Un autómata celular puede ser utilizado para explorar diferentes combinaciones de tratamientos y encontrar la mejor estrategia personalizada para un paciente específico. Al ajustar las reglas del autómata celular según las características del paciente y el tipo de tumor, se pueden identificar las terapias más efectivas para ese caso particular.
 - Investigación de resistencia a los tratamientos: Los autómatas celulares también pueden ayudar a investigar y comprender la resistencia a los tratamientos del cáncer. Al simular la evolución del tumor en presencia de tratamientos específicos, es posible identificar las características y los mecanismos que pueden llevar a la resistencia, lo que podría guiar el desarrollo de enfoques terapéuticos más efectivos.

## Problemas
### Como manejar grandes cantidades de celulas y sus conexiones?
### Como simular un proceso tan costoso y con tantos parametros?
### Como representar estas celulas y sus conexiones para analizarlas?
### Como representar la estructura del tumor?

## Si, son muchos problemas.

### Dando solucion a los problemas
#### Manejar mucha informacion.
- Uso de archivos .json para manejar la informacion y los parametros a asignar.
- Uso de lenguajes de programacion rapidos y eficientes como C# para el manejo de memoria y guardado de gran cantidad de informacion en memoria o disco.
- Empleo de matrices, grafos, estructuras similares.
- Empleo de estructuras que manejen busquedas rapidas como diccionarios o tablas de hash.

![Ejemplo archivo .json con configuraciones](json.png "Ejemplo archivo .json con configuraciones")

#### Simulacion
-Se lleva a cabo la implementacion de un motor eficiente que nos permita simular cualquier tumor que se origine en el tejido epitelial de cualquier organo.
- Para esto se lleva a cabo el analisis region por region del organo que se este analizando.
- El rendimiento, la velocidad y el alcance dependeran de los recursos con los que cuente el ordenador en el que se este ejecutando la simulacion.

#### Visualizar estructura de organos, tejidos, celular y conexiones.

Python y Streamlit. Se utilizan librerias para trabajar con grafos y representarlos en 3D, gravis, networkx.

#Visualización de las células y las conexiones entre las mismas


![Visualización de las células y las conexiones entre las mismas](graphs.gif "Visualización de las células y las conexiones entre las mismas")

#### Representacion del tumor
Unity
- Se implementa un algoritmo para representar cada cara exterior del tumor. 
- Tener en cuenta que al disponer de tantos vertices se dificulta la representacion del tumor, asi que una forma eficiente de representar esto es mediante alguns tecnicas para el modelado en 3D.

Marching Cubes - https://people.eecs.berkeley.edu/~jrs/meshpapers/LorensenCline.pdf

#Renderizacion del tumor

![Renderizacion del tumor](tumor.gif "Renderizacion del tumor")

## Cuanto hemos avanzado hasta el momento?
- Carga y utilizacion de parametros para la simulacion.
- Visualizacion y Analisis del grafo de celulas con sus conexiones.
- Visualizacion del tamanno que tomara el tumor a lo largo de la simulacion.


## Cual sera nuestro enfoque en las siguientes etapas?
Mejorar todo lo anterior y ademas:
- Leer mucha informacion medica.
- Implementar cambios entre estados siguiendo los detalles encontrados en la literatura.
- En un futuro agregar Inteligencia Artificial y tecnicas de Machine Learning para obtener mejores aproximaciones a la realidad.
# Roadmap (Hoja de Ruta)
```mermaid
graph LR;
    Teoria-->Partes_De_Organo-->Tejidos;
    here[(Estamos aqui)]
    here_sim[(Estamos aqui)]
    here_vis[(Estamos aqui)]
    Automata_Celular-->Teoria;
    Automata_Celular-->Manejo_Representación_Estructura;
    Automata_Celular-->Simulación_del_proceso;
    Automata_Celular--> Representación_y_Visualización;
    Manejo_Representación_Estructura-->Red_de_mundo_pequeño;
    Red_de_mundo_pequeño-->Visualizar_red_de_mp;
    Visualizar_red_de_mp-->Python_Libreria_Para_Grafos;
    Red_de_mundo_pequeño-->here;
    here--> Órgano;
    Organo-->Regiones_Del_organo;
    Organo-->Celula;
    Celula-->Region_de_Celulas_con_Posiciones;
    Region_de_Celulas_con_Posiciones-->Manejador_de_Regiones;
    Region_de_Celulas-->Celulas_reoresentadas_por_celdas-->Celdas;
    Celdas-->Posicion_y_Estado;
    Simulación_del_proceso-->Parametros_de_Simulacion;
    Simulación_del_proceso-->Probabilidades_y_Variables_Aleatorias;
    Simulación_del_proceso-->Informacion_Relevante;
    Simulación_del_proceso-->Transiciones_de_Estados-->here_sim;
    Representación_y_Visualización --> Busqueda_de_algoritmo_eficiente;
    Busqueda_de_algoritmo_eficiente --> Pruebas_de_renderizacion;
    Busqueda_de_algoritmo_eficiente --> here_vis --> Pruebas_de_reproduccion_de_cambios_en_tiempo_real;
    Representación_y_Visualización--> Busqueda_de_motor_grafico;
    Busqueda_de_motor_grafico --> Unity;
```
## Descripcion
El proyecto se divide en 3 ramas principales. La rama de la estructura de nuestro automata celular, la rama de la simulacion del proceso en general del crecimiento del automata y por ultimo la rama referente a todo el proceso de visualizacion del proceso, la parte grafica. Se agrega una rama referente a toda la teoria detras de nuestro modelo.

Si se sigue la trayectoria de cada una de las 3 ramas se podra notar que se encuentra un indicador de donde se esta investigando actualmente (representado con el texto Estamos aqui).
### Manejo - Representación - Estructura
Se comienza Creando Un Organo principal y Uno o mas Organos secundarios. Cada uno de estos organos tendra cierta esctura, la cual sera una red de mundo pequenno para representar las celulas y sus conexiones.

### Simulación del proceso
Contiene todo lo relacionado con la transicion de estados y aplicacion de los parametros de simulacion y variables aleatorias. Es el nucleo del proyecto y se centra en lograr una aproximacion a la realidad en cuanto al desarrollo y crecimiento del tumor.

Se comienza definiendo ciertas clases y estructuras para representar y trabajar con:
- Parametros_de_Simulacion
- Probabilidades_y_Variables_Aleatorias
- Informacion_Relevante: Informacion sobre los alrededores de las celulas.
- Transiciones_de_Estados

Luego se comenzara a desallorrar lo relacionado con la transicion de estados. Recalcar que una transicion entre un estado y otro tendra en cuenta parametros, probabilidades y variables aleatorias y la informacion relevante.
### Representación y Visualización
Se utiliza como herramienta el framework Unity, en C#. Se mapeara la region comprendida por el tumor en un instante de tiempo. Mas adelante se pretende mapear toda la simulacion del proceso en tiempo real.


Destacar que se prevee que el proyecto se encuentre en constantes cambios. Asi que la informacion que se encuentra actualmente puede que cambio parcialmente.

## Agradecimientos:
- A mi tutor Reynaldo.
- A los creadores de semejante genialidad: https://github.com/dionisio35/CausalFlow/ @lauolivia y @dionisio35
- A la creadora de la implementacion del algoritmo de Marching Cubes: https://github.com/omarvision/how-todo-marching-cubes @omarvision