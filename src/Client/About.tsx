import { useRef, useState } from "react";
import ReactDOM from "react-dom";
import { createRoot } from 'react-dom/client'
import { Canvas, MeshProps, useFrame } from '@react-three/fiber'
import { Mesh } from "three";
import React from "react";

function Box(props) {
    // This reference will give us direct access to the mesh
    const mesh = useRef<Mesh>(null!)
    // Set up state for the hovered and active state
    const [hovered, setHover] = useState(false)
    const [active, setActive] = useState(false)
    const f = mesh?.current?.rotation!

    // Subscribe this component to the render-loop, rotate the mesh every frame
    useFrame((state, delta) =>(mesh.current.rotation.x += delta))
    // Return view, these are regular three.js elements expressed in JSX
    return (
      <mesh
        {...props}
        ref={mesh}
        scale={active ? 1.5 : 1}
        onClick={(event) => setActive(!active)}
        onPointerOver={(event) => setHover(true)}
        onPointerOut={(event) => setHover(false)}>
        <boxGeometry args={[1, 1, 1]} />
        <meshPhongMaterial color={hovered ? 'white' : 'red'} />
      </mesh>
    )
  }
  
interface IAboutProps { title : string ; }
export const About = (props:IAboutProps  ) => {
    console.log(props.title)
    return (
        <Canvas>
        <ambientLight intensity={10}  />
        <pointLight intensity={3} position={[20, 10, 10]} />
        <Box position={[-1.2, 0, 0]} />
        <Box position={[1.2, 0, 0]} />
      </Canvas>)
}